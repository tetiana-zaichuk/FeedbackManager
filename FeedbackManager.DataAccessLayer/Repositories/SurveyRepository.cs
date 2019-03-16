using FeedbackManager.DataAccessLayer.Data;
using FeedbackManager.DataAccessLayer.Interfaces;

namespace FeedbackManager.DataAccessLayer.Repositories
{
    public class SurveyRepository : Repository<Entities.Survey, int>, ISurveyRepository
    {
        public SurveyRepository(FeedbackManagerDbContext context) : base(context) { }
    }
}
