using System.Collections.Generic;

namespace FeedbackManager.Shared.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }

        public string QuestionName { get; set; }

        public string ShortComment { get; set; }

        public int SurveyId { get; set; }

        public SurveyDto Survey { get; set; }

        public List<string> Answers { get; set; }
    }
}
