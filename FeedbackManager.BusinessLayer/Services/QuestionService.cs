using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FeedbackManager.Shared.Dtos;
using FeedbackManager.BusinessLayer.Interfaces;
using FeedbackManager.DataAccessLayer.Interfaces;

namespace FeedbackManager.BusinessLayer.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public QuestionService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public bool QuestionValidation(QuestionDto question) => !string.IsNullOrWhiteSpace(question.QuestionName) && question.SurveyId != 0 && question.Answers.Any();

        public async Task<IEnumerable<QuestionDto>> GetAllEntitiesAsync()
        {
            var entities = await _uow.QuestionRepository.GetAsync();
            return ConvertService.ConvertToListAnswers(entities, _mapper);
        }

        public async Task<QuestionDto> GetEntityByIdAsync(int id)
        {
            var entities = await _uow.QuestionRepository.GetAsync(o => o.Id == id);
            return entities == null ? null : ConvertService.ConvertToListAnswers(entities, _mapper).FirstOrDefault();
        }

        public async Task<QuestionDto> CreateEntityAsync(QuestionDto request)
        {
            var entity = ConvertService.ConvertToStringAnswers(request, _mapper);
            entity = await _uow.QuestionRepository.CreateAsync(entity);
            var result = await _uow.SaveAsync();
            request.Id = entity.Id;
            return !result || entity == null ? null : request;
        }

        public async Task<bool> UpdateEntityByIdAsync(QuestionDto request, int id)
        {
            var entity = ConvertService.ConvertToStringAnswers(request, _mapper);
            var updated = await _uow.QuestionRepository.UpdateAsync(entity);
            return await _uow.SaveAsync();
        }

        public async Task<bool> DeleteEntityByIdAsync(int id)
        {
            await _uow.QuestionRepository.DeleteAsync(id);
            return await _uow.SaveAsync();
        }
    }
}
