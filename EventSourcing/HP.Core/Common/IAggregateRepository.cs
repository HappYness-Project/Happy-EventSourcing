using HP.Core.Models;
namespace HP.Core.Common
{
    public interface IAggregateRepository<T> where T : IAggregateRoot
    {
        Task PersistAsync(T aggregateRoot, CancellationToken ct = default);
        Task<T> RehydrateAsync(Guid id, CancellationToken ct = default);
        Task<List<Guid>> GetAggregateIdAsync();
    }
}
