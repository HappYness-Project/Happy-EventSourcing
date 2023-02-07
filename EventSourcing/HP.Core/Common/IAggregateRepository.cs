namespace HP.Core.Common
{
    public interface IAggregateRepository<T>
    {
        Task PersistAsync(T aggregateRoot, CancellationToken ct = default);
        Task<T> RehydrateAsync(Guid id, CancellationToken ct = default);
    }

}
