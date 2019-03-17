using System;
using System.Net;
using FakeItEasy;
using FeedbackManager.BusinessLayer.Interfaces;
using FeedbackManager.Controllers;
using FeedbackManager.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;

namespace FeedbackManagerController.Tests
{
    [TestFixture]
    public class SurveyTests
    {
        private SurveyController _surveyController;
        private readonly ISurveyService _fakeSurveyService;
        private SurveyDto _survey;

        public SurveyTests() => _fakeSurveyService = A.Fake<ISurveyService>();

        [SetUp]
        public void TestSetup()
        {
            _survey = new SurveyDto { Id = 1, CreatorName = "Jose Moreno", CreatedAt = DateTime.UtcNow,
                SurveyName = "Survey1", Description = "Some text 1", Questions = null};

            _surveyController = new SurveyController(_fakeSurveyService);
        }

        [TearDown]
        public void TestTearDown() { }

        [Test]
        public void Get_Should_ReturnOk_When_Called()
        {
            var result = _surveyController.Get().Result;
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [Test]
        public void Get_Should_ReturnNotFound_When_Called()
        {
            var result = _surveyController.Get().Result;
            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Test]
        public void GetById_Should_ReturnOk_When_Called()
        {
            A.CallTo(() => _fakeSurveyService.GetEntityByIdAsync(_survey.Id)).Returns(_survey);
            var result = _surveyController.GetById(_survey.Id).Result;
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [Test]
        public void GetById_Should_ReturnNotFound_When_Called()
        {
            var result = _surveyController.GetById(_survey.Id).Result;
            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Test]
        public void Create_Should_ReturnOk_When_Called()
        {
            var result = _surveyController.Create(_survey).Result;
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            A.CallTo(() => _fakeSurveyService.CreateEntityAsync(_survey)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Create_Should_ReturnInternalServerError_When_SurveyIsNull()
        {
            var result = _surveyController.Create(null).Result;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
        
        [Test]
        public void Create_Should_ReturnBadRequest_When_SurveyHasId()
        {
            var result = _surveyController.Create(_survey).Result;
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Test]
        public void Update_Should_ReturnInternalServerError_When_Called()
        {
            var result = _surveyController.Update(_survey.Id, _survey).Result;
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
            A.CallTo(() => _fakeSurveyService.DeleteEntityByIdAsync(_survey.Id)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Update_Should_ReturnNoContent_When_Called()
        {
            var result = _surveyController.Update(_survey.Id, _survey).Result;
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            A.CallTo(() => _fakeSurveyService.DeleteEntityByIdAsync(_survey.Id)).MustNotHaveHappened();
        }

        [Test]
        public void Delete_Should_ReturnInternalServerError_When_Called()
        {
            var result = _surveyController.Delete(_survey.Id).Result;
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
            A.CallTo(() => _fakeSurveyService.DeleteEntityByIdAsync(_survey.Id)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Delete_Should_ReturnNoContent_When_Called()
        {
            var result = _surveyController.Delete(_survey.Id).Result;
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            A.CallTo(() => _fakeSurveyService.DeleteEntityByIdAsync(_survey.Id)).MustNotHaveHappened();
        }
    }
}
