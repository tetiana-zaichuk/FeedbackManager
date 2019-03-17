using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FeedbackManager;
using FeedbackManager.Shared.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FeedbackManagerFunctional.Tests
{
    public class SurveyTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private const string Path = "https://localhost:44351/Survey/";
        private SurveyDto _survey;

        public SurveyTests()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [SetUp]
        public void TestSetup()
        {
            _survey = new SurveyDto { Id = 1, CreatorName = "Jose Moreno", CreatedAt = DateTime.UtcNow,
                SurveyName = "Survey1", Description = "Some text 1", Questions = null};
        }

        [TearDown]
        public void TestTearDown() { }

        [Test]
        public async Task TestGetSurveys()
        {
            var response = await _client.GetAsync(Path);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<SurveyDto>>(responseString);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task TestGetById()
        {
            var response = await _client.GetAsync(Path + _survey.Id);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SurveyDto>(responseString);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(_survey.Id, result.Id);
                Assert.AreEqual(_survey.CreatorName, result.CreatorName);
                Assert.AreEqual(_survey.SurveyName, result.SurveyName);
            });
        }

        [Test]
        public async Task TestCreateSurvey()
        {
            _survey.Id = 0;
            _survey.SurveyName = "Survey 7";
            string json = await Task.Run(() => JsonConvert.SerializeObject(_survey));
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(Path, httpContent);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SurveyDto>(responseString);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(_survey.CreatorName, result.CreatorName);
                Assert.AreEqual(_survey.SurveyName, result.SurveyName);
            });
        }

        [Test]
        public async Task TestUpdateSurvey()
        {
            string json = await Task.Run(() => JsonConvert.SerializeObject(_survey));
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(Path + _survey.Id, httpContent);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SurveyDto>(responseString);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task TestDeleteById()
        {
            var response = await _client.DeleteAsync(Path + _survey.Id);
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
