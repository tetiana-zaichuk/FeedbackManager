namespace FeedbackManager.DataAccessLayer.Entities
{
    public class Question : Entity<int>
    {
        public override int Id { get; set; }

        public string QuestionName { get; set; }

        public string ShortComment { get; set; }

       public int SurveyId { get; set; }

        public Survey Survey { get; set; }

        public string Answers { get; set; }
    }
}
