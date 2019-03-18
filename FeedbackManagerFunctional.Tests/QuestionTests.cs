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
    public class QuestionTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private const string Path = "https://localhost:44351/Question/";
        private QuestionDto _question;

        public QuestionTests()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [SetUp]
        public void TestSetup()
        {
            _question = new QuestionDto
            {
                Id = 2,
                SurveyId = 1,
                QuestionName = "Question title 2",
                Answers = new List<string> { "Answer 1", "Answer 2"},
                ShortComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
            };
        }

        [TearDown]
        public void TestTearDown() { }

        [Test]
        public async Task TestGetQuestions()
        {
            var response = await _client.GetAsync(Path);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<QuestionDto>>(responseString);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task TestGetById()
        {
            var response = await _client.GetAsync(Path + _question.Id);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<QuestionDto>(responseString);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(_question.Id, result.Id);
                Assert.AreEqual(_question.SurveyId, result.SurveyId);
                Assert.AreEqual(_question.QuestionName, result.QuestionName);
            });
        }

        [Test]
        public async Task TestCreateSurvey()
        {
            _question.Id = 0;
            Random rnd = new Random();
            _question.QuestionName = "Question " + rnd.Next(50);
            string json = await Task.Run(() => JsonConvert.SerializeObject(_question));
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(Path, httpContent);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<QuestionDto>(responseString);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(_question.SurveyId, result.SurveyId);
                Assert.AreEqual(_question.QuestionName, result.QuestionName);
            });
        }

        [Test]
        public async Task TestUpdateSurvey()
        {
            string json = await Task.Run(() => JsonConvert.SerializeObject(_question));
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(Path + _question.Id, httpContent);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<QuestionDto>(responseString);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task TestDeleteById()
        {
            //You can write an id that exist in database instead 3
            var response = await _client.DeleteAsync(Path + "3");
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
