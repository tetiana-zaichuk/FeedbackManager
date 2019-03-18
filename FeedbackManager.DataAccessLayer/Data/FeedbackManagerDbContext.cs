using Microsoft.EntityFrameworkCore;
using FeedbackManager.DataAccessLayer.Entities;

namespace FeedbackManager.DataAccessLayer.Data
{
    public class FeedbackManagerDbContext : DbContext
    {
        public FeedbackManagerDbContext(DbContextOptions<FeedbackManagerDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Survey>().HasMany(u => u.Questions).WithOne(n => n.Survey).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Seed();
        }

        public DbSet<Survey> Survey { get; set; }
        public DbSet<Question> Question { get; set; }
    }
}
