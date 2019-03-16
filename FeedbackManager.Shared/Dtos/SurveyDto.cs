using System;
using System.Collections.Generic;

namespace FeedbackManager.Shared.Dtos
{
    public class SurveyDto
    {
        public int Id { get; set; }

        public string CreatorName { get; set; }

        public string SurveyName { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<QuestionDto> Questions { get; set; }
    }
}
