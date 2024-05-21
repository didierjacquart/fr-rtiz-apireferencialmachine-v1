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
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Context;
using BDM.ReferencialMachine.DataAccess.Models;
using BDM.ReferencialMachine.DataAccess.UnitTest.Data;
using Newtonsoft.Json;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow;
using Xunit;

namespace BDM.ReferencialMachine.Api.IntegrationTest.Features
{
    [Binding]
    public class ClauseManagmentSteps : IClassFixture<TestFixture<Startup>>
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly HttpClient _client;
        private readonly MachineContext _context;

        public ClauseManagmentSteps(TestFixture<Startup> testFixture, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _context = testFixture.WebApplicationFactory.Services.GetService(typeof(MachineContext)) as MachineContext;
            _client = testFixture.WebApplicationFactory.CreateClient();
        }
        private HttpRequestMessage BuildRequest(HttpMethod verb, string jsonContent, string uri)
        {
            var request = new HttpRequestMessage(verb, uri);
            if (jsonContent != null)
            {
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }
            return request;
        }

        private HttpRequestMessage BuildRequest(HttpMethod verb, string uri)
        {
            return BuildRequest(verb, null, uri);
        }

        [Given(@"J ai en base une liste de 3 clauses")]
        public void GivenJAiEnBaseUneListeDe3Clauses()
        {
            var dbClauses = MachineBuilder.FakeDbEditionCLause().Generate(3);
            _scenarioContext["dbClauses"] = dbClauses;
            _context.AddRange(dbClauses);            
            _context.SaveChanges();
        }

        [Given(@"J ai en base une liste de 3 clauses liees a une machine")]
        public void GivenJAiEnBaseUneListeDe3ClausesLieesAUneMachine()
        {
            var dbClauses = MachineBuilder.FakeDbEditionCLause().Generate(3);
            var dbMachine = MachineBuilder.FakeDbMachine(dbClauses).Generate(1).First();
            _scenarioContext["dbClauses"] = dbClauses;
            _scenarioContext["dbMachines"] = dbMachine;
            _context.AddRange(dbClauses);
            _context.Add(dbMachine);
            _context.SaveChanges();
        }

        [When(@"je demande la liste des clauses")]
        public async Task WhenJeDemandeLaListeDesClauses()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/clauses");
            var response = await _client.SendAsync(request);
            _scenarioContext["response"] = response;
        }

        [Then(@"Je retourne HTTP 200 avec la liste des clauses")]
        public async Task ThenJeRetourneHTTP200AvecLaListeDesClauses()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);

            Assert.Contains(MediaTypeNames.Application.Json, response.Content.Headers.ContentType.ToString());

            var responseStream = await response.Content.ReadAsStreamAsync();
            var reader = new JsonTextReader(new StreamReader(responseStream))
            {
                DateParseHandling = DateParseHandling.None
            };
            var serializer = new JsonSerializer();
            var editionClauses = serializer.Deserialize<IEnumerable<EditionClause>>(reader);
            
            Assert.NotNull(editionClauses);

            var expectedClauses = _scenarioContext["dbClauses"] as IList<T_EDITION_CLAUSE>;
            Assert.Equal(expectedClauses.Count(), editionClauses.Count());
            
            foreach (var expectedClause in expectedClauses)
            {
                var clauseResult = editionClauses.FirstOrDefault(x => x.Code == expectedClause.CODE);
                Assert.NotNull(clauseResult);
                Assert.Equal(expectedClause.DESCRIPTION, clauseResult.Description);
                Assert.Equal(expectedClause.LABEL, clauseResult.Label);
                Assert.Equal(expectedClause.TYPE, clauseResult.Type);
            }
        }

        [Given(@"J ai en en entree le flux d une clause")]
        public void GivenJAiEnEnEntreeLeFluxDUneClause()
        {
            var clause = new EditionClause
            {
                Code = "MAXX", 
                Label = "mon label", 
                Type = "VOL", 
                Description = "une description"
            };
            _scenarioContext["clause"] = clause;
        }

        [When(@"je demande la creation d une clause")]
        public async Task WhenJeDemandeLaCreationDUneClause()
        {
            var clause = _scenarioContext.Get<EditionClause>("clause");
            var request = BuildRequest(HttpMethod.Post, clause.ToJson(), "/api/clauses");
            var response = await _client.SendAsync(request);
            _scenarioContext["response"] = response;
        }

        [Given(@"J ai en en entree le flux d une clause existante")]
        public void GivenJAiEnEnEntreeLeFluxDUneClauseExistante()
        {
            var dbClausesList = _scenarioContext.Get<IList<T_EDITION_CLAUSE>>("dbClauses");
            var codeAlreadyUsed = dbClausesList.First().CODE;

            var existingClause = new EditionClause
            {
                Code = codeAlreadyUsed,
                Label = "mon label",
                Description = "ma desription",
                Type = "VOL"
            };
            _scenarioContext["clause"] = existingClause;
            _scenarioContext["clauseCode"] = null;
            _scenarioContext["anomalyCode"] = ExceptionConstants.CLAUSE_ALREADY_EXIST_EXCCEPTION_CODE;
            _scenarioContext["anomalyMessage"] = ExceptionConstants.CLAUSE_ALREADY_EXIST_EXCCEPTION_MESSAGE(existingClause.Code);
        }

        [Then(@"Je retourne un code HTTP 400 badrequest")]
        public async Task ThenJeRetourneUnCodeHTTP400Badrequest()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            Assert.Contains(MediaTypeNames.Application.Json, response.Content.Headers.ContentType.ToString());

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


        [Given(@"J ai en en entree le flux d une clause sans le champ (.*)")]
        public void GivenJAiEnEnEntreeLeFluxDUneClauseSansLeChampLabel(string name)
        {
            var clause = new EditionClause
            {
                Type = "VOL",
                Description = "une description"
            };
            switch (name)
            {
                case "label":
                    clause.Code = "MAXX";
                    break;
                case "code":
                    clause.Label = "label";
                    break;
                default:
                    throw new Exception("field name not recognized");
            }

            _scenarioContext["clause"] = clause;
            _scenarioContext["anomalyCode"] = ExceptionConstants.CLAUSE_CODE_AND_LABEL_MANDATORY_EXCEPTION_CODE;
            _scenarioContext["anomalyMessage"] = ExceptionConstants.CLAUSE_CODE_AND_LABEL_MANDATORY_EXCEPTION_MESSAGE;
        }

        [When(@"je demande la mise a jour d une clause")]
        public async Task WhenJeDemandeLaMiseAJourDUneClause()
        {
            var clause = _scenarioContext.Get<EditionClause>("clause");
            var clauseCode = _scenarioContext.Get<string>("clauseCode");
            clauseCode ??= clause.Code;
            var request = BuildRequest(HttpMethod.Put, clause.ToJson(), $"/api/clauses/{clauseCode}");
            var response = await _client.SendAsync(request);
            _scenarioContext["response"] = response;
        }

        [Then(@"Je retourne un code HTTP 201 created")]
        public async Task ThenJeRetourneUnCodeHTTP201Created()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            Assert.Contains(MediaTypeNames.Application.Json, response.Content.Headers.ContentType.ToString());

            var responseStream = await response.Content.ReadAsStreamAsync();
            var reader = new JsonTextReader(new StreamReader(responseStream))
            {
                DateParseHandling = DateParseHandling.None
            };
            var serializer = new JsonSerializer();
            var editionClause = serializer.Deserialize<EditionClause>(reader);

            Assert.NotNull(editionClause);

        }

        [Then(@"Je retourne un code HTTP 200 ok")]
        public void ThenJeRetourneUnCodeHTTP200Ok()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Given(@"J ai en en entree le flux d une clause inexistante")]
        public void GivenJAiEnEnEntreeLeFluxDUneClauseInexistante()
        {
            var clause = new EditionClause
            {
                Code = "MAXX",
                Label = "mon label",
                Type = "VOL",
                Description = "une description"
            };
            _scenarioContext["clause"] = clause;
            _scenarioContext["clauseCode"] = null;
            _scenarioContext["anomalyCode"] = ExceptionConstants.CLAUSE_NOT_FOUND_EXCEPTION_CODE;
            _scenarioContext["anomalyMessage"] = ExceptionConstants.CLAUSE_NOT_FOUND_EXCEPTION_MESSAGE(clause.Code);
        }

        [Then(@"Je retourne un code HTTP 404 not found")]
        public async Task ThenJeRetourneUnCodeHTTP404NotFound()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>("response");
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            Assert.Contains(MediaTypeNames.Application.Json, response.Content.Headers.ContentType.ToString());

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

        [Given(@"J ai en en entree le flux d une clause existante avec un code errone")]
        public void GivenJAiEnEnEntreeLeFluxDUneClauseExistanteAvecUnCodeErrone()
        {
            var dbClausesList = _scenarioContext.Get<IList<T_EDITION_CLAUSE>>("dbClauses");
            var codeAlreadyUsed = dbClausesList.First().CODE;

            var existingClause = new EditionClause
            {
                Code = codeAlreadyUsed,
                Label = "mon label",
                Description = "ma desription",
                Type = "VOL"
            };
            _scenarioContext["clauseCode"] = "MAXX";
            _scenarioContext["clause"] = existingClause;
            _scenarioContext["anomalyCode"] = ExceptionConstants.CLAUSE_CODE_DIFFERENT_FROM_OBJECT_EDITION_CLAUSE_EXCEPTION_CODE;
            _scenarioContext["anomalyMessage"] = ExceptionConstants.CLAUSE_CODE_DIFFERENT_FROM_OBJECT_EDITION_CLAUSE_EXCEPTION_MESSAGE;

        }

        [Given(@"J ai en en entree le code d une clause existante")]
        public void GivenJAiEnEnEntreeLeCodeDUneClauseExistante()
        {
            var dbClausesList = _scenarioContext.Get<IList<T_EDITION_CLAUSE>>("dbClauses");
            _scenarioContext["clauseCode"] = dbClausesList.First().CODE;
        }

        [When(@"je demande la suppression d une clause")]
        public async Task WhenJeDemandeLaSuppressionDUneClause()
        {
            var clauseCode = _scenarioContext.Get<string>("clauseCode");
            var request = BuildRequest(HttpMethod.Delete, $"/api/clauses/{clauseCode}");
            var response = await _client.SendAsync(request);
            _scenarioContext["response"] = response;
        }

        [Given(@"J ai en en entree le code d une clause inexistante")]
        public void GivenJAiEnEnEntreeLeCodeDUneClauseInexistante()
        {
            _scenarioContext["clauseCode"] = "MAXX";
            _scenarioContext["anomalyCode"] = ExceptionConstants.CLAUSE_NOT_FOUND_EXCEPTION_CODE;
            _scenarioContext["anomalyMessage"] = ExceptionConstants.CLAUSE_NOT_FOUND_EXCEPTION_MESSAGE("MAXX");
        }

        [Given(@"J ai en en entree le code d une clause existante attachee a une machine")]
        public void GivenJAiEnEnEntreeLeCodeDUneClauseExistanteAttacheeAUneMachine()
        {
            var dbClausesList = _scenarioContext.Get<IList<T_EDITION_CLAUSE>>("dbClauses");
            _scenarioContext["clauseCode"] = dbClausesList.First().CODE;
            _scenarioContext["anomalyCode"] = ExceptionConstants.MACHINE_ATTTACHED_TO_EDITION_CLAUSE_EXCEPTION_CODE;
            _scenarioContext["anomalyMessage"] = ExceptionConstants.MACHINE_ATTTACHED_TO_EDITION_CLAUSE_EXCEPTION_MESSAGE(dbClausesList.First().CODE);
        }
    }
}
