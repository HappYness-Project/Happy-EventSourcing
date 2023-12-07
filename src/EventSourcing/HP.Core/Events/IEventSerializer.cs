using HP.Core.Models;
namespace HP.Core.Events
{
    public interface IEventSerializer
    {
        IDomainEvent Deserialize(string type, byte[] data);
        IDomainEvent Deserialize(string type, string data);
        byte[] Serialize(IDomainEvent @event);
    }
}
