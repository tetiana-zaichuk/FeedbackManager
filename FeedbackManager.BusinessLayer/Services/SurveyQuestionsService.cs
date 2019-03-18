using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FeedbackManager.BusinessLayer.Interfaces;
using FeedbackManager.DataAccessLayer.Interfaces;
using FeedbackManager.Shared.Dtos;

namespace FeedbackManager.BusinessLayer.Services
{
    public class SurveyQuestionsService : ISurveyQuestionsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SurveyQuestionsService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public bool QuestionValidation(QuestionDto question) => !string.IsNullOrWhiteSpace(question.QuestionName) && question.Answers.Any();

        public async Task<IEnumerable<QuestionDto>> GetAllEntitiesBySurveyIdAsync(int surveyId)
        {
            var entities = await _uow.QuestionRepository.GetAsync(o => o.SurveyId == surveyId);
            return ConvertService.ConvertToListAnswers(entities, _mapper);
        }
        
        public async Task<QuestionDto> CreateEntityAsync(int surveyId, QuestionDto request)
        {
            var entity = ConvertService.ConvertToStringAnswers(request, _mapper);
            entity = await _uow.QuestionRepository.CreateAsync(entity);
            var result = await _uow.SaveAsync();
            request.Id = entity.Id;
            if (!result || entity == null)
            {
                return null;
            }
            return request;
        }
        
        public async Task<bool> DeleteEntityByIdAsync(int id)
        {
            await _uow.QuestionRepository.DeleteAsync(id);
            return await _uow.SaveAsync();
        }
    }
}
