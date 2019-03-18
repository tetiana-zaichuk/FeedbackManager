using System.ComponentModel.DataAnnotations;
using FeedbackManager.DataAccessLayer.Interfaces;

namespace FeedbackManager.DataAccessLayer.Entities
{
    public abstract class Entity<T> : IEntity<T>
    {
        [Key]
        public abstract T Id { get; set; }
    }
}
