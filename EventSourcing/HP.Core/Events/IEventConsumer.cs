using HP.Core.Models;

namespace HP.Core.Events
{
    public interface IEventConsumer
    {
        void Consumer(string topicName);
    }
}
