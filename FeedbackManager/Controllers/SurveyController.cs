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
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService service) => _surveyService = service;

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<SurveyDto>>> Get()
        {
            var dtos = await _surveyService.GetAllEntitiesAsync();
            if (!dtos.Any())
            {
                return NoContent();
            }
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<SurveyDto>> GetById(int id)
        {
            var dto = await _surveyService.GetEntityByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpPost]
        public virtual async Task<ActionResult<SurveyDto>> Create([FromBody] SurveyDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dto = await _surveyService.CreateEntityAsync(request);
            if (dto == null)
            {
                return StatusCode(500);
            }
            return dto;
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Update([FromRoute] int id, [FromBody] SurveyDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _surveyService.UpdateEntityByIdAsync(request, id);
            if (!result)
            {
                return StatusCode(500);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(int id)
        {
            var result = await _surveyService.DeleteEntityByIdAsync(id);
            if (!result)
            {
                return StatusCode(500);
            }
            return NoContent();
        }
    }
}
