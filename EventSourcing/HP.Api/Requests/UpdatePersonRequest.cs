namespace HP.Api.Requests
{
    public record UpdatePersonRequest(string FirstName, string LastName, string Email);
    public record UpdateRoleRequest(string Role);

}
