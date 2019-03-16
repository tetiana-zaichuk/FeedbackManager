using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FeedbackManager.DataAccessLayer.Entities;
using FeedbackManager.Shared.Dtos;

namespace FeedbackManager.BusinessLayer.Services
{
    public static class ConvertService
    {
        public static IEnumerable<QuestionDto> ConvertToListAnswers(List<Question> entities, IMapper _mapper)
        {
            var result = _mapper.Map<List<Question>, List<QuestionDto>>(entities);
            for (var i = 0; i < result.Count; i++)
            {
                result[i].Answers = entities[i].Answers.Split(';').ToList();
            }
            return result;
        }

        public static Question ConvertToStringAnswers(QuestionDto request, IMapper _mapper)
        {
            var entity = _mapper.Map<QuestionDto, Question>(request);
            entity.Answers = String.Join(";", request.Answers);
            return entity;
        }
    }
}
