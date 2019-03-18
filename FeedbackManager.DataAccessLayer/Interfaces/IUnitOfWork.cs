using System.Threading.Tasks;

namespace FeedbackManager.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        Task BeginTransaction();

        void CommitTransaction();

        ISurveyRepository SurveyRepository { get; }

        IQuestionRepository QuestionRepository { get; }

        Task<bool> SaveAsync();
    }
}
