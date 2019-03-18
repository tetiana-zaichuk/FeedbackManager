using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FeedbackManager.DataAccessLayer.Data;
using FeedbackManager.DataAccessLayer.Interfaces;
using FeedbackManager.DataAccessLayer.Repositories;

namespace FeedbackManager.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FeedbackManagerDbContext _context;

        private ISurveyRepository _surveyRepository;
        private IQuestionRepository _questionRepository;

        public UnitOfWork(FeedbackManagerDbContext context) => _context = context;

        public async Task BeginTransaction() => await _context.Database.BeginTransactionAsync();

        public void CommitTransaction() => _context.Database.CommitTransaction();

        public ISurveyRepository SurveyRepository => _surveyRepository ?? (_surveyRepository = new SurveyRepository(_context));

        public IQuestionRepository QuestionRepository => _questionRepository ?? (_questionRepository = new QuestionRepository(_context));

        public async Task<bool> SaveAsync()
        {
            try
            {
                var changes = _context.ChangeTracker.Entries().Count(
                    p => p.State == EntityState.Modified || p.State == EntityState.Deleted
                                                         || p.State == EntityState.Added);
                return changes == 0 || await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
