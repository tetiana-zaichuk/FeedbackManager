using FeedbackManager.DataAccessLayer.Data;
using FeedbackManager.DataAccessLayer.Entities;
using FeedbackManager.DataAccessLayer.Interfaces;

namespace FeedbackManager.DataAccessLayer.Repositories
{
    public class QuestionRepository : Repository<Question, int>, IQuestionRepository
    {
        public QuestionRepository(FeedbackManagerDbContext context) : base(context) { }
    }
}
