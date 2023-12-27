using HP.Core.Common;
using HP.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure.Services
{
    public class UserUniqueChecker : IUserUniqueChecker
    {
        private IBaseRepository<UserUniqueness> _repository;
        public UserUniqueChecker(IBaseRepository<UserUniqueness> baseRepository)
        {
            _repository = baseRepository;
        }

        public async Task CreateAsync(string email, string displayName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email value cannot be null or whitespace.", nameof(email));

            await _repository.CreateAsync(new UserUniqueness { Email = email, DisplayName = displayName });
        }

        public async Task<bool> IsEmailUnique(string userEmail, CancellationToken cancellationToken = default)
            => await _repository.Exists(o => o.Email == userEmail);

    }
    public record UserUniqueness
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
}
