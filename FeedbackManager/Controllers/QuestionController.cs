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
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService service) => _questionService = service;

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<QuestionDto>>> Get()
        {
            var dtos = await _questionService.GetAllEntitiesAsync();
            if (!dtos.Any())
            {
                return NoContent();
            }
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<QuestionDto>> GetById(int id)
        {
            var dto = await _questionService.GetEntityByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpPost]
        public virtual async Task<ActionResult<QuestionDto>> Create([FromBody] QuestionDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dto = await _questionService.CreateEntityAsync(request);
            if (dto == null)
            {
                return StatusCode(500);
            }
            return dto;
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Update([FromRoute] int id, [FromBody] QuestionDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _questionService.UpdateEntityByIdAsync(request, id);
            if (!result)
            {
                return StatusCode(500);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(int id)
        {
            var result = await _questionService.DeleteEntityByIdAsync(id);
            if (!result)
            {
                return StatusCode(500);
            }
            return NoContent();
        }
    }
}