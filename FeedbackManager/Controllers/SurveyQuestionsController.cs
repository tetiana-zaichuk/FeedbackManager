using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<IEnumerable<QuestionDto>>> Get(int surveyId)
        {
            var dtos = await _surveyQuestionsService.GetAllEntitiesBySurveyIdAsync(surveyId);
            if (!dtos.Any())
            {
                return NoContent();
            }
            return Ok(dtos);
        }
        
        [HttpPost("{surveyId}")]
        public async Task<ActionResult<QuestionDto>> Create([FromRoute] int surveyId, [FromBody] QuestionDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dto = await _surveyQuestionsService.CreateEntityAsync(surveyId, request);
            if (dto == null)
            {
                return StatusCode(500);
            }
            return dto;
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _surveyQuestionsService.DeleteEntityByIdAsync(id);
            if (!result)
            {
                return StatusCode(500);
            }
            return NoContent();
        }
    }
}