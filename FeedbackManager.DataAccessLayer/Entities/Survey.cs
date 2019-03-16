using System;
using System.Collections.Generic;

namespace FeedbackManager.DataAccessLayer.Entities
{
    public class Survey : Entity<int>
    {
        public override int Id { get; set; }

        public string CreatorName { get; set; }

        public string SurveyName { get; set; }
        
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<Question> Questions { get; set; }
    }
}
