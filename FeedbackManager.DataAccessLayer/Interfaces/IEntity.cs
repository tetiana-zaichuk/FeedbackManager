namespace FeedbackManager.DataAccessLayer.Interfaces
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
