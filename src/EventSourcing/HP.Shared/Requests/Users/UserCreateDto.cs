namespace HP.Shared.Requests.Users
{
    public class CreateUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public HashSet<string> Permissions { get; set; }

    }
}
