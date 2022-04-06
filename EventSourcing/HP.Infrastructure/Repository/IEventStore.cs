using Microsoft.EntityFrameworkCore.Storage;

namespace HP.Infrastructure
{
    public interface IEventStore
    {
        void Init();
        public bool AppendEvent<TStream>(Guid streamId, object @event, long? expectedVersion = null) where TStream : notnull;
    }

}