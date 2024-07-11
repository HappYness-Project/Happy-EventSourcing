namespace HP.Domain.People
{
    public interface IUserUniqueChecker
    {
        Task<bool> IsEmailUnique(string userEmail, CancellationToken cancellationToken = default);
        Task CreateAsync(string email, string displayName, CancellationToken cancellationToken = default);
    }
}
