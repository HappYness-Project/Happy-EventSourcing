namespace HP.Core.Events
{
    public interface IEventConsumer
    {
        Task Consumer(string topicName);
    }
}
