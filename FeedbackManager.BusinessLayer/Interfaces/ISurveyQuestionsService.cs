using System.Collections.Generic;
using System.Threading.Tasks;
using FeedbackManager.Shared.Dtos;

namespace FeedbackManager.BusinessLayer.Interfaces
{
    public interface ISurveyQuestionsService
    {
        bool QuestionValidation(QuestionDto question);

        Task<IEnumerable<QuestionDto>> GetAllEntitiesBySurveyIdAsync(int id);
        
        Task<QuestionDto> CreateEntityAsync(int id, QuestionDto request);

        Task<bool> DeleteEntityByIdAsync(int id);
    }
}
