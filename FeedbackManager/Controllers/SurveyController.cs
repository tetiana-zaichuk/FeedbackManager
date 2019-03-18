using System.Collections.Generic;
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
        public virtual async Task<ObjectResult> Get()
        {
            var dtos = await _surveyService.GetAllEntitiesAsync();
            return !dtos.Any() ? new ObjectResult(NoContent()) : Ok(dtos);
        }

        [HttpGet("{id}")]
        public virtual async Task<ObjectResult> GetById(int id)
        {
            if (id == 0) return new ObjectResult(NotFound());
            var dto = await _surveyService.GetEntityByIdAsync(id);
            return dto == null ? new ObjectResult(NotFound()) : Ok(dto);
        }

        [HttpPost]
        public virtual async Task<ObjectResult> Create([FromBody] SurveyDto request)
        {
            if (!ModelState.IsValid || request==null)
            {
                return BadRequest(ModelState);
            }

            var dto = await _surveyService.CreateEntityAsync(request);
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public virtual async Task<HttpResponseMessage> Update([FromRoute] int id, [FromBody] SurveyDto request)
        {
            var result = await _surveyService.UpdateEntityByIdAsync(request, id);
            return !result ? new HttpResponseMessage(HttpStatusCode.InternalServerError) : new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var result = await _surveyService.DeleteEntityByIdAsync(id);
            return !result ? new HttpResponseMessage(HttpStatusCode.InternalServerError) : new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
