using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FeedbackManager.Shared.Dtos;
using FeedbackManager.BusinessLayer.Interfaces;
using FeedbackManager.DataAccessLayer.Interfaces;
using FeedbackManager.DataAccessLayer.Entities;

namespace FeedbackManager.BusinessLayer.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SurveyService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SurveyDto>> GetAllEntitiesAsync()
        {
            var entities = await _uow.SurveyRepository.GetAsync();
            return _mapper.Map<List<Survey>, List<SurveyDto>>(entities);
        }

        public async Task<SurveyDto> GetEntityByIdAsync(int id)
        {
            var entity = await _uow.SurveyRepository.GetAsync(o => o.Id == id);
            return entity == null ? null : _mapper.Map<Survey, SurveyDto>(entity.FirstOrDefault());
        }

        public async Task<SurveyDto> CreateEntityAsync(SurveyDto request)
        {
            var entity = _mapper.Map<SurveyDto, Survey>(request);
            entity = await _uow.SurveyRepository.CreateAsync(entity);
            var result = await _uow.SaveAsync();
            return !result || entity == null ? null : _mapper.Map<Survey, SurveyDto>(entity);
        }

        public async Task<bool> UpdateEntityByIdAsync(SurveyDto request, int id)
        {
            var entity = _mapper.Map<SurveyDto, Survey>(request);
            entity.Id = id;
            var updated = await _uow.SurveyRepository.UpdateAsync(entity);
            return await _uow.SaveAsync();
        }

        public async Task<bool> DeleteEntityByIdAsync(int id)
        {
            await _uow.SurveyRepository.DeleteAsync(id);
            return await _uow.SaveAsync();
        }
    }
}
