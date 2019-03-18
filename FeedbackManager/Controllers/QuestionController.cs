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
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService service) => _questionService = service;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dtos = await _questionService.GetAllEntitiesAsync();
            if (!dtos.Any())
            {
                return NoContent();
            }
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _questionService.GetEntityByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ObjectResult> Create([FromBody] QuestionDto request)
        {
            if (!_questionService.QuestionValidation(request))
            {
                return BadRequest("Please fill out these fields: question title, survey id and answers.");
            }

            var dto = await _questionService.CreateEntityAsync(request);
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] QuestionDto request)
        {
            if (!_questionService.QuestionValidation(request))
            {
                return BadRequest("Please fill out these fields: question title, survey id and answers.");
            }

            var result = await _questionService.UpdateEntityByIdAsync(request, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var result = await _questionService.DeleteEntityByIdAsync(id);
            return !result ? new HttpResponseMessage(HttpStatusCode.InternalServerError) : new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}