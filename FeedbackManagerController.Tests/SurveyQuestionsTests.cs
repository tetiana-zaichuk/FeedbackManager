using System.Collections.Generic;
using System.Net;
using FakeItEasy;
using FeedbackManager.BusinessLayer.Interfaces;
using FeedbackManager.Controllers;
using FeedbackManager.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace FeedbackManagerController.Tests
{
    [TestFixture]
    public class SurveyQuestionsTests
    {
        private SurveyQuestionsController _surveyQuestionsController;
        private readonly ISurveyQuestionsService _fakeSurveyQuestionsService;
        private QuestionDto _question;

        public SurveyQuestionsTests() => _fakeSurveyQuestionsService = A.Fake<ISurveyQuestionsService>();

        [SetUp]
        public void TestSetup()
        {
            _question = new QuestionDto
            {
                Id = 1,
                SurveyId = 1,
                QuestionName = "Question title 1",
                Answers = new List<string> { "Answer 1", "Answer 2", "Answer 3" },
                ShortComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed dolore magna aliqua."
            };

            _surveyQuestionsController = new SurveyQuestionsController(_fakeSurveyQuestionsService);
        }

        [TearDown]
        public void TestTearDown() { }

        [Test]
        public void Get_Should_ReturnOk_When_Called()
        {
            A.CallTo(() => _fakeSurveyQuestionsService.GetAllEntitiesBySurveyIdAsync(_question.Id)).Returns(new List<QuestionDto> { _question });
            var result = _surveyQuestionsController.Get(_question.Id).Result;
            Assert.NotNull(result);
            var resultS = result as OkObjectResult;
            Assert.AreEqual(200, resultS.StatusCode);
        }

        [Test]
        public void Get_Should_ReturnNotFound_When_Called()
        {
            A.CallTo(() => _fakeSurveyQuestionsService.GetAllEntitiesBySurveyIdAsync(0)).Returns(new List<QuestionDto> {});
            var result = _surveyQuestionsController.Get(0).Result as NoContentResult;
            Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public void Create_Should_ReturnOk_When_Called()
        {
            A.CallTo(() => _fakeSurveyQuestionsService.QuestionValidation(_question)).Returns(true);
            var result = _surveyQuestionsController.Create(_question.SurveyId, _question).Result;
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            A.CallTo(() => _fakeSurveyQuestionsService.CreateEntityAsync(_question.SurveyId, _question)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Create_Should_ReturnBadRequest_When_QuestionHasEmptyRequiredFields()
        {
            _question.QuestionName = "";
            A.CallTo(() => _fakeSurveyQuestionsService.QuestionValidation(_question)).Returns(false);
            var result = _surveyQuestionsController.Create(_question.SurveyId, _question).Result;
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Test]
        public void Delete_Should_ReturnInternalServerError_When_Called()
        {
            A.CallTo(() => _fakeSurveyQuestionsService.DeleteEntityByIdAsync(0)).Returns(false);
            var result = _surveyQuestionsController.Delete(_question.Id).Result;
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
            A.CallTo(() => _fakeSurveyQuestionsService.DeleteEntityByIdAsync(0)).MustNotHaveHappened();
        }

        [Test]
        public void Delete_Should_ReturnNoContent_When_Called()
        {
            A.CallTo(() => _fakeSurveyQuestionsService.DeleteEntityByIdAsync(_question.Id)).Returns(true);
            var result = _surveyQuestionsController.Delete(_question.Id).Result;
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }
    }
}
