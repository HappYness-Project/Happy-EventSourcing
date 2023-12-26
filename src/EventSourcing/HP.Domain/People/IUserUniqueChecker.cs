namespace HP.Domain.People
{
    public interface IUserUniqueChecker
    {
        bool IsUniqueUser(string userName, string userEmail);
    }
}
