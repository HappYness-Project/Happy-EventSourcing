
using HP.Domain.Common;

namespace HP.Domain
{
    public class Person : Entity, IAggregateRoot
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public static Person Create()
        {
            //if (!email.IsValid) throw new EmailNotValidException(email);
            //bool unique = userUniqueChecker.CheckAsync(email, cancellationToken).GetAwaiter().GetResult();
            //if (!unique) throw new UserAlreadyExistException(email);

            //UserId userId = userIdGenerator.Generate();
            //Password password = passwordGenerator.Generate();
            //PasswordHash passwordHash = passwordHasher.Hash(password);
            //var user = new User(userId, email, passwordHash, DateTime.UtcNow);
            //var userCreatedEvent = new UserCreatedEvent(user, password);
            return new Person();
        }

    }
}
