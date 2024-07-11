using HP.Core.Models;
namespace HP.Core.Common
{
    public interface IAggregateRepository<T> where T : IAggregateRoot
    {
        Task PersistAsync(T aggregateRoot, CancellationToken ct = default);
        Task<T> RehydrateAsync<T>(Guid id, CancellationToken ct = default) where T : AggregateRoot, new();
    }
}
