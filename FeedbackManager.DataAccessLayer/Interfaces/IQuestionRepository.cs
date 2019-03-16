using FeedbackManager.DataAccessLayer.Entities;

namespace FeedbackManager.DataAccessLayer.Interfaces
{
    public interface IQuestionRepository : IRepository<Question, int>
    {
    }
}
