using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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
    public class QuestionTests
    {
        private QuestionController _questionController;
        private readonly IQuestionService _fakeQuestionService;
        private QuestionDto _question;

        public QuestionTests() => _fakeQuestionService = A.Fake<IQuestionService>();

        [SetUp]
        public void TestSetup()
        {
            _question = new QuestionDto
            {
                Id = 1,
                SurveyId = 1,
                QuestionName = "Question title 1",
                Answers = new List<string>{"Answer 1","Answer 2","Answer 3"},
                ShortComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed dolore magna aliqua."
            };
            
            _questionController = new QuestionController(_fakeQuestionService);
        }

        [TearDown]
        public void TestTearDown() { }

        [Test]
        public void Get_Should_ReturnOk_When_Called()
        {
            A.CallTo(() => _fakeQuestionService.GetAllEntitiesAsync()).Returns(new List<QuestionDto> { _question });
            var result = _questionController.Get().Result;
            Assert.NotNull(result);
            var resultS = result as OkObjectResult;
            Assert.AreEqual(200, resultS.StatusCode);
        }

        [Test]
        public void Get_Should_ReturnNotFound_When_Called()
        {
            A.CallTo(() => _fakeQuestionService.GetAllEntitiesAsync()).Returns(new List<QuestionDto> {});
            var result = _questionController.Get().Result as NoContentResult;
            Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public void GetById_Should_ReturnOk_When_Called()
        {
            A.CallTo(() => _fakeQuestionService.GetEntityByIdAsync(_question.Id)).Returns(_question);
            var result = _questionController.GetById(_question.Id).Result;
            Assert.NotNull(result);
            var resultS = result as OkObjectResult;
            Assert.AreEqual(200, resultS.StatusCode);
        }

        [Test]
        public void GetById_Should_ReturnNotFound_When_Called()
        {
            A.CallTo(() => _fakeQuestionService.GetEntityByIdAsync(0)).Returns(Task.FromResult<QuestionDto>(null));
            var result = _questionController.GetById(0).Result as NotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void Create_Should_ReturnOk_When_Called()
        {
            A.CallTo(() => _fakeQuestionService.QuestionValidation(_question)).Returns(true);
            var result = _questionController.Create(_question).Result;
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            A.CallTo(() => _fakeQuestionService.CreateEntityAsync(_question)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Create_Should_ReturnBadRequest_When_QuestionHasEmptyRequiredFields()
        {
            _question.QuestionName = "";
            A.CallTo(() => _fakeQuestionService.QuestionValidation(_question)).Returns(false);
            var result = _questionController.Create(_question).Result;
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Test]
        public void Update_Should_ReturnBadRequest_When_QuestionHasEmptyRequiredFields()
        {
            A.CallTo(() => _fakeQuestionService.QuestionValidation(_question)).Returns(false);
            var result = _questionController.Update(_question.Id, _question).Result as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Test]
        public void Update_Should_ReturnNoContent_When_Called()
        {
            A.CallTo(() => _fakeQuestionService.QuestionValidation(_question)).Returns(true);
            A.CallTo(() => _fakeQuestionService.UpdateEntityByIdAsync(_question, _question.Id)).Returns(true);
            var result = _questionController.Update(_question.Id, _question).Result as NoContentResult;
            Assert.AreEqual(StatusCodes.Status204NoContent, result.StatusCode);
            A.CallTo(() => _fakeQuestionService.UpdateEntityByIdAsync(_question, _question.Id)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Delete_Should_ReturnInternalServerError_When_Called()
        {
            A.CallTo(() => _fakeQuestionService.DeleteEntityByIdAsync(0)).Returns(false);
            var result = _questionController.Delete(_question.Id).Result;
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
            A.CallTo(() => _fakeQuestionService.DeleteEntityByIdAsync(0)).MustNotHaveHappened();
        }

        [Test]
        public void Delete_Should_ReturnNoContent_When_Called()
        {
            A.CallTo(() => _fakeQuestionService.DeleteEntityByIdAsync(_question.Id)).Returns(true);
            var result = _questionController.Delete(_question.Id).Result;
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }
    }
}
