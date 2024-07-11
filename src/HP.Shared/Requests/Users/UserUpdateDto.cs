using HP.Domain;
namespace HP.Shared.Requests.Users
{
    public record UserUpdateDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; } = null;
    }
}
