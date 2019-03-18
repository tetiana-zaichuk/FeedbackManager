using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeedbackManager.BusinessLayer.Interfaces;
using FeedbackManager.Shared.Dtos;

namespace FeedbackManager.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class SurveyQuestionsController : Controller
    {
        private readonly ISurveyQuestionsService _surveyQuestionsService;

        public SurveyQuestionsController(ISurveyQuestionsService service) => _surveyQuestionsService = service;
        
        [HttpGet("{surveyId}")]
        public async Task<IActionResult> Get(int surveyId)
        {
            var dtos = await _surveyQuestionsService.GetAllEntitiesBySurveyIdAsync(surveyId);
            if (!dtos.Any())
            {
                return NoContent();
            }
            return Ok(dtos);
        }
        
        [HttpPost("{surveyId}")]
        public async Task<ObjectResult> Create([FromRoute] int surveyId, [FromBody] QuestionDto request)
        {
            if (!_surveyQuestionsService.QuestionValidation(request))
            {
                return BadRequest("Please fill out these fields: question title and answers.");
            }

            var dto = await _surveyQuestionsService.CreateEntityAsync(surveyId, request);
            return Ok(dto);
        }
        
        [HttpDelete("{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var result = await _surveyQuestionsService.DeleteEntityByIdAsync(id);
            return !result ? new HttpResponseMessage(HttpStatusCode.InternalServerError) : new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}