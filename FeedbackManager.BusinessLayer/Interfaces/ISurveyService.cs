using System.Collections.Generic;
using System.Threading.Tasks;
using FeedbackManager.Shared.Dtos;

namespace FeedbackManager.BusinessLayer.Interfaces
{
    public interface ISurveyService
    {
        bool SurveyValidation(SurveyDto survey);

        Task<IEnumerable<SurveyDto>> GetAllEntitiesAsync();

        Task<SurveyDto> GetEntityByIdAsync(int id);

        Task<SurveyDto> CreateEntityAsync(SurveyDto entity);

        Task<bool> UpdateEntityByIdAsync(SurveyDto request, int id);

        Task<bool> DeleteEntityByIdAsync(int id);
    }
}
