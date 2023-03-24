namespace Identity.DTOs
{
    public class UpdateUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public HashSet<string>? Permissions { get; set; }
        public HashSet<Uri> RedirectUris { get; set; }
    }
}