using HP.Core.Common;
using HP.Core.Models;
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
        private IBaseRepository<UserAccountStorage> _repository;
        public UserUniqueChecker(IBaseRepository<UserAccountStorage> baseRepository)
        {
            _repository = baseRepository;
        }

        public async Task CreateAsync(string email, string displayName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email value cannot be null or whitespace.", nameof(email));

            await _repository.CreateAsync(new UserAccountStorage { Email = email, DisplayName = displayName });
        }

        public async Task<bool> IsEmailUnique(string userEmail, CancellationToken cancellationToken = default)
            => await _repository.Exists(o => o.Email == userEmail);

    }
    public class UserAccountStorage : BaseEntity
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
}
