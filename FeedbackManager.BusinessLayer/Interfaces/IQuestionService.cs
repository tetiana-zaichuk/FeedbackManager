using System.Collections.Generic;
using System.Threading.Tasks;
using FeedbackManager.Shared.Dtos;

namespace FeedbackManager.BusinessLayer.Interfaces
{
    public interface IQuestionService
    {
        bool QuestionValidation(QuestionDto question);

        Task<IEnumerable<QuestionDto>> GetAllEntitiesAsync();

        Task<QuestionDto> GetEntityByIdAsync(int id);

        Task<QuestionDto> CreateEntityAsync(QuestionDto entity);

        Task<bool> UpdateEntityByIdAsync(QuestionDto request, int id);

        Task<bool> DeleteEntityByIdAsync(int id);
    }
}
