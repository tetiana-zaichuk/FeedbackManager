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
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService service) => _surveyService = service;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dtos = await _surveyService.GetAllEntitiesAsync();
            if (!dtos.Any())
                return NoContent();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _surveyService.GetEntityByIdAsync(id);
            if (dto == null)
                return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ObjectResult> Create([FromBody] SurveyDto request)
        {
            if (!_surveyService.SurveyValidation(request))
            {
                return BadRequest("Please fill out these fields: creator name and survey title.");
            }

            var dto = await _surveyService.CreateEntityAsync(request);
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SurveyDto request)
        {
            var result = await _surveyService.UpdateEntityByIdAsync(request, id);
            if (!_surveyService.SurveyValidation(request))
            {
                return BadRequest("Please fill out these fields: creator name and survey title.");
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var result = await _surveyService.DeleteEntityByIdAsync(id);
            return !result ? new HttpResponseMessage(HttpStatusCode.InternalServerError) : new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
