using HP.Shared;
using HP.Shared.Contacts;

namespace HP.UserBusiness
{
    public class UserManager : IUserManager
    {
        public async Task<User> TrySignInAndGetUserAsync(User user)
        {
            //await Task.Delay(10000);
            Console.WriteLine("Hi from user manager.");
            return await Task.FromResult(new User());
        }
        public async Task InsertUserAsync(User user)
        {
            await Task.FromResult(true);
        }

    }
}