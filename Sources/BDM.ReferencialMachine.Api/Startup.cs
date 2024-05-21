using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using AF.WSIARD.AspNetCore.OAuth2TokenValidation;
using AF.WSIARD.AspNetCore.OAuth2TokenValidation.Authentication;
using AF.WSIARD.Exception.Middleware;
using AxaFrance.E2ELogging;
using AxaFrance.E2ELogging.AspNetCore;
using BDM.ReferencialMachine.Api.Configuration;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Interfaces;
using BDM.ReferencialMachine.Core.Services;
using BDM.ReferencialMachine.DataAccess.Context;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Mappers;
using BDM.ReferencialMachine.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using AF.WSIARD.AspNetCore.HealthCheck.Extensions;


namespace BDM.ReferencialMachine.Api
{
    public class Startup
    {
        private readonly SwaggerConfiguration _swaggerConfiguration;
        private readonly AuthenticationOptions _authentificationOptions;
        private readonly IConfigurationSection _machinesCriteriasOptions;
        private const int MaxAgeConfHsts = 365;
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _swaggerConfiguration = Configuration.GetSection("Swagger").Get<SwaggerConfiguration>();
            _authentificationOptions = Configuration.GetSection("Authentication").Get<AuthenticationOptions>();
            _authentificationOptions.Scopes = new[] { Scopes.BdmReferencialMachine };
            _machinesCriteriasOptions = Configuration.GetSection("MachinesListCriterias");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddE2ELogging();
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddMaamValidator(_authentificationOptions);
            services.AddExceptions(options => { options.UseBuiltInExceptionCatalog = true; });
            services.AddMemoryCache();

            SetUpDataBase(services);
            ConfigureSwagger(services);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13 | SecurityProtocolType.Tls12;

            services.AddHsts(options =>
            {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(MaxAgeConfHsts);
            });
            services.AddControllers().AddControllersAsServices()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.DateParseHandling = DateParseHandling.None;
                });
            
            services.Configure<MachinesCriteriasOptions>(_machinesCriteriasOptions);
            services.AddSingleton<IMapperPricingRate, MapperPricingRate>();
            services.AddSingleton<IMapperFamilies, MapperFamilies>();
            services.AddSingleton<IMapperEditionClauses, MapperEditionClauses>();
            services.AddSingleton<IMapperDatabaseToBusiness, MapperDatabaseToBusiness>();
            services.AddSingleton<IMapperBusinessToDatabase, MapperBusinessToDatabase>();
            services.AddSingleton<IMapperRiskPrecision, MapperRiskPrecision>();

            services.AddTransient<IMachineRepository, MachineRepository>();
            services.AddTransient<IClauseRepository, ClauseRepository>();
            services.AddTransient<IClauseByMachineRepository, ClauseByMachineRepository>();
            services.AddTransient<IFamilyRepository, FamilyRepository>();

            services.AddTransient<IMachineService, MachineService>();
            services.AddTransient<IClauseService, ClauseService>();
            services.AddTransient<IFamilyService, FamilyService>();

            services.AddHealthCheckVersionLiveness(Configuration);
            services.AddHealthCheckDefaultStartup();
        }

        private void SetUpDataBase(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("machineContext")!;
            services.AddDbContextPool<MachineContext>(options =>
            {
                options.UseSqlServer(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.SwaggerDoc(_swaggerConfiguration.Version, new OpenApiInfo
                {
                    Title = _swaggerConfiguration.Title,
                    Version = _swaggerConfiguration.Version,
                    Contact = new OpenApiContact
                    {
                        Name = _swaggerConfiguration.ContactName,
                        Email = string.Empty,
                    },
                    License = new OpenApiLicense
                    {
                        Name = _swaggerConfiguration.LicenseName
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swaggerGenOptions.IncludeXmlComments(xmlPath);

                if (_authentificationOptions.Type == AuthentificationType.None)
                {
                    return;
                }

                swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                    Scheme = "bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                });
                swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseE2ELogging();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }
            else
            {
                app.UseHsts();
            }

            app.UseExceptionMiddleware();
            
            // Ajout des headers sécurité
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Remove("X-Powered-By");

                AddResponseHeaders(context, "X-Xss-Protection", "1; mode=block");
                AddResponseHeaders(context, "X-Content-Type-Options", "nosniff");
                AddResponseHeaders(context, "X-Frame-Options", "DENY");
                AddResponseHeaders(context, "Content-Security-Policy", "child-src *.axa-fr.intraxa");

                await next();
            });

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(swaggerUiOptions =>
            {
                swaggerUiOptions.SwaggerEndpoint(string.Format(_swaggerConfiguration.JsonEndpoint, _swaggerConfiguration.Version), _swaggerConfiguration.Title);
                swaggerUiOptions.RoutePrefix = _swaggerConfiguration.UiEndpoint;
            });

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();                
                endpoints.MapHealthCheckPathsForLiveness();
                endpoints.MapHealthCheckPathsForStartup();
            });
        }

        private static void AddResponseHeaders(HttpContext context, string headerName, string headerValue)
        {
            if (context.Response.Headers.ContainsKey(headerName))
            {
                context.Response.Headers.Remove(headerName);
            }
            context.Response.Headers.Append(headerName, headerValue);
        }
    }
}
