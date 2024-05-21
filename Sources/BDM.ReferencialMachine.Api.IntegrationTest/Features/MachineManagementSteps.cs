using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using AF.WSIARD.Exception.Middleware.Model;
using AutoFixture;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Context;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Mappers;
using BDM.ReferencialMachine.DataAccess.Models;
using BDM.ReferencialMachine.DataAccess.UnitTest.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow;
using Xunit;
using MC = BDM.ReferencialMachine.Core.Constants.MachineConstants;

namespace BDM.ReferencialMachine.Api.IntegrationTest.Features
{
    [Binding]
    public class MachineManagementSteps : IClassFixture<TestFixture<Startup>>
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly HttpClient _client;
        private readonly MachineContext _context;
        private readonly IMapperDatabaseToBusiness _mapperDatabaseToBusiness; 
        private readonly Fixture _fixture;


        public MachineManagementSteps(TestFixture<Startup> testFixture, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _context = testFixture.WebApplicationFactory.Services.GetService(typeof(MachineContext)) as MachineContext;
            _mapperDatabaseToBusiness = testFixture.WebApplicationFactory.Services.GetService(typeof(IMapperDatabaseToBusiness)) as IMapperDatabaseToBusiness;
            _client = testFixture.WebApplicationFactory.CreateClient();
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Given(@"a valid machineCode '(.*)'")]
        public void GivenAValidMachineCode(string machineCode)
        {
            _context.Add(new T_MACHINE_SPECIFICATION { 
                CODE = machineCode, 
                NAME = "",
                START_DATETIME_SUBSCRIPTION_PERIOD = new DateTime(2000,1,1),
                LABEL = "Ma machine"});
            _context.SaveChanges();
            _scenarioContext.Set(machineCode, "machineCode");
        }

        [Given(@"an unknown machineCode '(.*)'")]
        public void GivenAnUnknownMachineCode(string machineCode)
        {
            _scenarioContext.Set(machineCode, "machineCode");
        }
        
        [When(@"the service receive a call of GET '(.*)'")]
        public async Task WhenTheServiceReceiveACallOf(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await _client.SendAsync(request);
            _scenarioContext["response"] = response;
        }       
        
        [Then(@"the service return an unknown result")]
        public void ThenTheServiceReturnAnUnknownResult()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Then(@"the service return an ok result with machineSpecication")]
        public async Task ThenTheServiceReturnAnOkResultWithMachineSpecication()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);

            Assert.Contains(MediaTypeNames.Application.Json, response.Content.Headers.ContentType!.ToString());

            var responseStream = await response.Content.ReadAsStreamAsync();
            var reader = new JsonTextReader(new StreamReader(responseStream))
            {
                DateParseHandling = DateParseHandling.None
            };

            var serializer = new JsonSerializer();
            var machineSpecification = serializer.Deserialize<MachineSpecification>(reader);

            Assert.NotNull(machineSpecification);
            var machineCode = (string)_scenarioContext["machineCode"];
            Assert.Equal(machineCode, machineSpecification.Code);
            Assert.Equal("Ma machine", machineSpecification.Label);
        }

        private HttpRequestMessage BuildRequest(string jsonContent, string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            return request;
        }

        [Given(@"a business machine fully filled")]
        public void GivenABusinessMachineFullyFilled()
        {
            _context.AddRange(T_FAMILY_Sample.GetDatabaseFamilies());
            _context.AddRange(T_EDITION_CLAUSE_Sample.GetDatabaseEditionClauses());
            _context.SaveChanges();
            var machine = MachineSpecification_Sample.GetMachineSpecification();
            _scenarioContext["jsonContent"] = machine.ToJson();
        }

        [When(@"the service receive a call of POST '(.*)'")]
        public async Task WhenTheServiceReceiveACallOfPOST(string uri)
        {
            var jsonContent = _scenarioContext["jsonContent"] as string;
            var request = BuildRequest(jsonContent, uri);
            var response = await _client.SendAsync(request);
            _scenarioContext["response"] = response;
        }

        [Then(@"the service return the guid of the new machine")]
        public async Task ThenTheServiceReturnTheGuidOfTheNewMachine()
        {
            var response = _scenarioContext["response"] as HttpResponseMessage;
            Assert.True(response!.IsSuccessStatusCode);
            Assert.Contains(MediaTypeNames.Application.Json, response.Content.Headers.ContentType!.ToString());

            var responseStream = await response.Content.ReadAsStreamAsync();
            var reader = new JsonTextReader(new StreamReader(responseStream))
            {
                DateParseHandling = DateParseHandling.None
            };

            var serializer = new JsonSerializer();
            var machine = serializer.Deserialize<MachineSpecification>(reader);
            Assert.NotNull(machine);

            var tMachineSpecifications = await _context.T_MACHINE_SPECIFICATION.Include(x => x.T_FAMILY).Include(x => x.T_PRICING_RATE).FirstOrDefaultAsync(x => x.CODE == machine.Code);

            Assert.NotNull(tMachineSpecifications);
            
            var expectedMachine = MachineSpecification_Sample.GetMachineSpecification();
            Assert.Equal(expectedMachine.Code, tMachineSpecifications!.CODE);
            Assert.Equal(expectedMachine.Label, tMachineSpecifications.LABEL);
            Assert.Equal(expectedMachine.Name, tMachineSpecifications.NAME);
            Assert.Equal(expectedMachine.Description, tMachineSpecifications.DESCRIPTION);
            Assert.Equal(string.Join("|",expectedMachine.Keywords!), tMachineSpecifications.KEYWORDS);
            Assert.Equal(expectedMachine.IsDelegated, tMachineSpecifications.IS_DELEGATED);
            Assert.Equal(expectedMachine.IsUnreferenced, tMachineSpecifications.IS_UNREFERENCED);
            Assert.Equal(expectedMachine.IsExcluded, tMachineSpecifications.IS_EXCLUDED);
            Assert.Equal(expectedMachine.IsTransportable, tMachineSpecifications.IS_TRANSPORTABLE);
            Assert.Equal(expectedMachine.AgeLimitAllowed, tMachineSpecifications.AGE_LIMIT_ALLOWED);
            Assert.Equal(expectedMachine.IsOutOfMachineInsurance, tMachineSpecifications.IS_OUT_OF_MACHINE_INSURANCE);
            Assert.Equal(expectedMachine.AllPlacesCovered, tMachineSpecifications.ALL_PLACE_CORVERED);
            Assert.Equal(expectedMachine.ExtendedFleetCoverageAllowedPercentage, tMachineSpecifications.EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE);
            Assert.Equal(expectedMachine.MachineRate, tMachineSpecifications.MACHINE_RATE);
            Assert.Equal(expectedMachine.Product, tMachineSpecifications.PRODUCT);
            Assert.Equal(expectedMachine.SubscriptionPeriod!.StartDateTime, tMachineSpecifications.START_DATETIME_SUBSCRIPTION_PERIOD);
            Assert.Equal(expectedMachine.SubscriptionPeriod!.EndDateTime, tMachineSpecifications.END_DATETIME_SUBSCRIPTION_PERIOD);

            var expectedFamilies = MachineFamilies_Sample.GetMachineFamilies();
            foreach (var excpectedFamily in expectedFamilies)
            {
                var families = tMachineSpecifications.T_FAMILY!.Where(x => x.CODE == excpectedFamily.Code).ToArray();
                Assert.NotEmpty(families);
                Assert.NotNull(families.FirstOrDefault(x => x.CODE == excpectedFamily.Code));
                Assert.NotNull(families.FirstOrDefault(x => x.NAME == excpectedFamily.Name));
                if (excpectedFamily.SubFamilies == null)
                {
                    continue;
                }
                foreach (var expectedSubFamily in excpectedFamily.SubFamilies)
                {
                    var subFamily = families!.FirstOrDefault(x => x.SUB_CODE == expectedSubFamily.Code);
                    Assert.NotNull(subFamily);
                    Assert.Equal(expectedSubFamily.Name, subFamily!.SUB_NAME);
                    Assert.Equal(expectedSubFamily.Code, subFamily.SUB_CODE);
                }
            }
            var expectedPricingRates = expectedMachine.PricingRates;
            Assert.Equal(expectedPricingRates!.Count, tMachineSpecifications.T_PRICING_RATE!.Count);
            foreach (var expectedPricingRate in expectedPricingRates)
            {
                var pricingRate =
                    tMachineSpecifications.T_PRICING_RATE.FirstOrDefault(x => x.CODE == MapperPricingRate.ConvertPrincingCodeToDataBase(expectedPricingRate.Code));
                Assert.NotNull(pricingRate);
                Assert.Equal(expectedPricingRate.Rate, pricingRate!.RATE);
            }
        }

        [Given(@"a list of stored machines")]
        public void GivenAListOfStoredMachines()
        {
            _context.AddRange(new List<T_MACHINE_SPECIFICATION>
            {
                new() { CODE = "1", NAME="", START_DATETIME_SUBSCRIPTION_PERIOD = new DateTime(2000,01,01), PRODUCT = "INVENTAIRE"},
                new() { CODE = "2", NAME="", START_DATETIME_SUBSCRIPTION_PERIOD = new DateTime(2000,01,01), PRODUCT = "Bob"},
                new() { CODE = "3", NAME="", START_DATETIME_SUBSCRIPTION_PERIOD = new DateTime(2000,01,01), PRODUCT = "INVENTAIRE"},
                new() { CODE = "4", NAME="", START_DATETIME_SUBSCRIPTION_PERIOD = new DateTime(2000,01,01), PRODUCT = "Bob"},
                new() { CODE = "5", NAME="", START_DATETIME_SUBSCRIPTION_PERIOD = new DateTime(2000,01,01), PRODUCT = "INVENTAIRE"},
                new() { CODE = "6", NAME="", START_DATETIME_SUBSCRIPTION_PERIOD = new DateTime(2000,01,01), PRODUCT = "Bob"},
                new() { CODE = "7", NAME="", START_DATETIME_SUBSCRIPTION_PERIOD = new DateTime(2000,01,01), PRODUCT = "Bob"},
                new() { CODE = "8", NAME="", START_DATETIME_SUBSCRIPTION_PERIOD = new DateTime(2000,01,01), PRODUCT = "INVENTAIRE"},
                new() { CODE = "9", NAME="", START_DATETIME_SUBSCRIPTION_PERIOD = new DateTime(2000,01,01), PRODUCT = "Bob"},
                new() { CODE = "10", NAME="", START_DATETIME_SUBSCRIPTION_PERIOD = new DateTime(2000,01,01), PRODUCT = "INVENTAIRE"}
            });
            
            _context.SaveChanges();
            var databaseMachine = _context.T_MACHINE_SPECIFICATION.ToArray();
            var expectedMachine = _mapperDatabaseToBusiness.Map(databaseMachine, null);
            _scenarioContext["StoredMachines"] = expectedMachine;

        }

        [Then(@"the service return the list of the machines")]
        public async Task ThenTheServiceReturnTheListOfTheMachines()
        {
            var response = _scenarioContext["response"] as HttpResponseMessage;
            Assert.True(response!.IsSuccessStatusCode);
            Assert.Contains(MediaTypeNames.Application.Json, response.Content.Headers.ContentType?.ToString()!);

            var responseStream = await response.Content.ReadAsStreamAsync();
            var reader = new JsonTextReader(new StreamReader(responseStream))
            {
                DateParseHandling = DateParseHandling.None
            };

            var serializer = new JsonSerializer();
            var machines = serializer.Deserialize<ICollection<MachineSpecification>>(reader);
            Assert.NotNull(machines);
            var expectedMachines = 
                (_scenarioContext["StoredMachines"] as ICollection<MachineSpecification>)?.Where(x => x.Product == MC.INVENTORY);
            Assert.NotNull(expectedMachines);
            Assert.Equal(expectedMachines.Count(), machines.Count);
        }

        [Then(@"the service return the list of the machines for the product '(.*)'")]
        public async Task ThenTheServiceReturnTheListOfTheMachinesForTheProduct(string product)
        {
            var response = _scenarioContext["response"] as HttpResponseMessage;
            Assert.True(response!.IsSuccessStatusCode);
            Assert.Contains(MediaTypeNames.Application.Json, response.Content.Headers.ContentType!.ToString());

            var responseStream = await response.Content.ReadAsStreamAsync();
            var reader = new JsonTextReader(new StreamReader(responseStream))
            {
                DateParseHandling = DateParseHandling.None
            };

            var serializer = new JsonSerializer();
            var machines = serializer.Deserialize<ICollection<MachineSpecification>>(reader);
            Assert.NotNull(machines);
            Assert.True(machines.All(x => x.Product == product));
            var expectedMachines = _scenarioContext["StoredMachines"] as ICollection<MachineSpecification>;
            Assert.NotNull(expectedMachines);
            Assert.Equal(expectedMachines.Count(x => x.Product == product), machines.Count);
        }

        [Given(@"J ai en base une liste de 3 clauses liees a 2 machines")]
        public void GivenJAiEnBaseUneListeDe3ClausesLieesA2Machines()
        {
            var dbClauses = MachineBuilder.FakeDbEditionCLause().Generate(3);
            var dbMachines = MachineBuilder.FakeDbMachine(dbClauses).Generate(2);
            _scenarioContext["dbClauses"] = dbClauses;
            _scenarioContext["dbMachines"] = dbMachines;
            _context.AddRange(dbClauses);
            _context.AddRange(dbMachines);
            _context.SaveChanges();
        }

        [Given(@"J ai en en entree les criteres de lecture")]
        public void GivenJAiEnEnEntreeLesCriteresDeLecture()
        {
            var dbMachineCodes = _scenarioContext.Get<IEnumerable<T_MACHINE_SPECIFICATION>>("dbMachines");

            var codes = dbMachineCodes.Select(x => x.CODE);
            var codesCollection = new Collection<string>();
            foreach (var code in codes)
            {
                codesCollection.Add(code);
            }

            var criterias = new MachineCriterias
            {
                MachineCodeList = codesCollection
            };
            _scenarioContext["criterias"] = criterias;
        }

        [When(@"je demande la lecture pour une liste de machines")]
        public async Task WhenJeDemandeLaLecturePourUneListeDeMachines()
        {
            var machineCriterias = _scenarioContext.Get<MachineCriterias>("criterias");
            var request = BuildRequest(machineCriterias.ToJson(), "/api/machines/clauses/list");
            var response = await _client.SendAsync(request);
            _scenarioContext["response"] = response;
        }
        
        [Then(@"Je retourne un code HTTP 200 et un tableau de code machine et liste de clauses")]
        public async Task ThenJeRetourneUnCodeHTTP200EtUnTableauDeCodeMachineEtListeDeClauses()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);

            Assert.Contains(MediaTypeNames.Application.Json, response.Content.Headers.ContentType?.ToString()!);

            var responseStream = await response.Content.ReadAsStreamAsync();
            var reader = new JsonTextReader(new StreamReader(responseStream))
            {
                DateParseHandling = DateParseHandling.None
            };
            var serializer = new JsonSerializer();

            var editionClausesByMachines = serializer.Deserialize<IEnumerable<EditionClausesByMachine>>(reader)!.ToArray();
            Assert.NotNull(editionClausesByMachines);

            var expectedMachines = _scenarioContext["dbMachines"] as IList<T_MACHINE_SPECIFICATION>;

            Assert.Equal(expectedMachines!.Count, editionClausesByMachines.Length);

            foreach (var expectedMachine in expectedMachines)
            {
                var editionClauseByMachine = editionClausesByMachines.FirstOrDefault(x => x.MachineCode == expectedMachine.CODE);
                Assert.NotNull(editionClauseByMachine);
                var expectedClauseCodes = expectedMachine.EDITION_CLAUSE_CODES.Split("|");

                Assert.Equal(expectedClauseCodes.Length, editionClauseByMachine.EditionClauseList.Count);
                foreach (var expectedClauseCode in expectedClauseCodes)
                {
                    Assert.True(editionClauseByMachine.EditionClauseList.Select(x => x.Code).ToList().Contains(expectedClauseCode));
                }
            }
        }



        [Given(@"J ai en en entree les criteres de lecture avec un code machine inconnu")]
        public void GivenJAiEnEnEntreeLesCriteresDeLectureAvecUnCodeMachineInconnu()
        {
            var dbMachineCodes = _scenarioContext.Get<IEnumerable<T_MACHINE_SPECIFICATION>>("dbMachines");
            var codes = dbMachineCodes.Select(x => x.CODE).ToArray();
            var codesCollection = new Collection<string> { codes[0], "code_inconnu" };

            var criterias = new MachineCriterias
            {
                MachineCodeList = codesCollection
            };
            _scenarioContext["criterias"] = criterias;
            _scenarioContext["anomalyCode"] = ExceptionConstants.MACHINE_NOT_FOUND_EXCEPTION_CODE;
            _scenarioContext["anomalyMessage"] = ExceptionConstants.MACHINES_NOT_FOUND_EXCEPTION_MESSAGE("code_inconnu");
        }

        [Given(@"J ai en en entree les criteres de lecture avec des codes identiques")]
        public void GivenJAiEnEnEntreeLesCriteresDeLectureAvecDesCodesIdentiques()
        {
            var dbMachineCodes = _scenarioContext.Get<IEnumerable<T_MACHINE_SPECIFICATION>>("dbMachines");
            var codes = dbMachineCodes.Select(x => x.CODE).ToArray();
            var codesCollection = new Collection<string> { codes[0], codes[0] };

            var criterias = new MachineCriterias
            {
                MachineCodeList = codesCollection
            };
            _scenarioContext["criterias"] = criterias;
            _scenarioContext["anomalyCode"] = ExceptionConstants.DUPLICATED_CODES_SENT_IN_REQUEST_CODE;
            _scenarioContext["anomalyMessage"] = ExceptionConstants.DUPLICATED_CODES_SENT_IN_REQUEST_MESSAGE(codes[0]);
        }

        [Given(@"J ai en en entree les criteres de lecture avec trop de codes")]
        public void GivenJAiEnEnEntreeLesCriteresDeLectureAvecTropDeCodes()
        {
            var codesCollection = new Collection<string> { "1", "2", "3", "4", "5", "6" };

            var criterias = new MachineCriterias
            {
                MachineCodeList = codesCollection
            };
            _scenarioContext["criterias"] = criterias;
            _scenarioContext["anomalyCode"] = ExceptionConstants.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_CODE;
            _scenarioContext["anomalyMessage"] = ExceptionConstants.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_MESSAGE(2);
        }

        [Then(@"Je retourne un code HTTP 413 requestEntityTooLarge")]
        public async Task ThenJeRetourneUnCodeHTTP413RequestEntityTooLarge()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.RequestEntityTooLarge, response.StatusCode);

            Assert.Contains(MediaTypeNames.Application.Json, response.Content.Headers.ContentType!.ToString());

            var responseStream = await response.Content.ReadAsStreamAsync();
            var reader = new JsonTextReader(new StreamReader(responseStream))
            {
                DateParseHandling = DateParseHandling.None
            };
            var serializer = new JsonSerializer();
            var anomaly = serializer.Deserialize<Anomaly>(reader);

            var expectedAnomalyCode = _scenarioContext.Get<string>("anomalyCode");
            var expectedAnomalyMessage = _scenarioContext.Get<string>("anomalyMessage");

            Assert.NotNull(anomaly);
            Assert.Equal(expectedAnomalyCode, anomaly.Code);
            Assert.Equal(expectedAnomalyMessage, anomaly.Label);
        }

        [Then(@"Je retourne un code HTTP (.*) et un tableau avec seulement les clauses de la machine connue")]
        public async Task ThenJeRetourneUnCodeHTTPEtUnTableauAvecSeulementLesClausesDeLaMachineConnue(int p0)
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);

            Assert.Contains(MediaTypeNames.Application.Json, response.Content.Headers.ContentType!.ToString());

            var responseStream = await response.Content.ReadAsStreamAsync();
            var reader = new JsonTextReader(new StreamReader(responseStream))
            {
                DateParseHandling = DateParseHandling.None
            };

            var serializer = new JsonSerializer();
            var editionClausesByMachines = serializer.Deserialize<IEnumerable<EditionClausesByMachine>>(reader);
            
            Assert.NotNull(editionClausesByMachines);
            Assert.Single(editionClausesByMachines);
        }

        [Given(@"a list of parks stored")]
        public async Task GivenAListOfParksStored()
        {
            var parks = _fixture.Build<T_MACHINE_SPECIFICATION>()
                .Without(x => x.END_DATETIME_SUBSCRIPTION_PERIOD)
                .CreateMany(5).ToList();

            _scenarioContext["dbParks"] = parks;

            var lstPrecisions = new List<T_RISK_PRECISION>();
            for(var i = 0; i < parks.Count; i++)
            {
                parks[i].PRODUCT = MC.PARK;            
                var dbRiskPrecisions = _fixture.CreateMany<T_RISK_PRECISION>(5).ToList();            
                dbRiskPrecisions = dbRiskPrecisions.Select(x => new T_RISK_PRECISION
                {
                    MACHINE_CODE = parks[i].CODE,
                    DETAIL = x.DETAIL,
                    LABEL = x.LABEL,
                    CODE = x.CODE
                }).ToList();
                lstPrecisions.AddRange(dbRiskPrecisions);
            }

            _scenarioContext["dbPrecisions"] = lstPrecisions;

            await _context.AddRangeAsync(parks);
            await _context.AddRangeAsync(lstPrecisions);
            await _context.SaveChangesAsync();
        }

        [Then(@"the service return a the list of the Parks")]
        public async Task ThenTheServiceReturnATheListOfTheParks()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);

            Assert.Contains(MediaTypeNames.Application.Json, response.Content.Headers.ContentType?.ToString()!);

            var responseStream = await response.Content.ReadAsStreamAsync();
            var reader = new JsonTextReader(new StreamReader(responseStream))
            {
                DateParseHandling = DateParseHandling.None
            };
            var serializer = new JsonSerializer();

            var parks = serializer.Deserialize<IEnumerable<MachineSpecification>>(reader)!.ToArray();
            Assert.NotNull(parks);

            var expectedParks = (_scenarioContext["dbParks"] as IList<T_MACHINE_SPECIFICATION>)?
                .Where(x => x.PRODUCT == MC.PARK).ToArray();

            var expectedPrecisions = (_scenarioContext["dbPrecisions"] as IList<T_RISK_PRECISION>).ToArray();

            Assert.Equal(expectedParks!.Length, parks.Length);
            var expectedPark = expectedParks.First();

            var park = parks.FirstOrDefault(x => x.Code == expectedPark.CODE);
            Assert.NotNull(park);

            Assert.Equal(expectedPark.CODE, park.Code);
            Assert.Equal(expectedPark.LABEL, park.Label);
            Assert.Equal(expectedPark.NAME, park.Name);
            
            Assert.Equal(expectedPark.IS_DELEGATED, park.IsDelegated);
            Assert.Equal(expectedPark.IS_UNREFERENCED, park.IsUnreferenced);
            Assert.Equal(expectedPark.IS_EXCLUDED, park.IsExcluded);
            Assert.Equal(expectedPark.IS_OUT_OF_MACHINE_INSURANCE, park.IsOutOfMachineInsurance);
            Assert.Equal(expectedPark.PRODUCT, park.Product);

            var expectedPrecisionForPark =  expectedPrecisions.First(x => x.MACHINE_CODE == expectedPark.CODE);
            Assert.Equal(expectedPrecisionForPark.CODE, park.RiskPrecisions.FirstOrDefault()?.Code);
            Assert.Equal(expectedPrecisionForPark.LABEL, park.RiskPrecisions.FirstOrDefault()?.Label);
            Assert.Equal(expectedPrecisionForPark.DETAIL, park.RiskPrecisions.FirstOrDefault()?.Detail);
            Assert.Null(park.RiskPrecisions.FirstOrDefault()!.MachineCode);
            Assert.Null(park.AgreementDeductible);

            
        }


    }
}
