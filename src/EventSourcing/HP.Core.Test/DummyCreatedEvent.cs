using HP.Core.Models;
namespace HP.Core.Test
{

    public class DummyCreatedEvent : DomainEvent
    {
        public string DummyName { get; set; }
        public int Score { get; set; }
    }
}
