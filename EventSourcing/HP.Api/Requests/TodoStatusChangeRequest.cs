namespace HP.Api.Requests
{
    public record TodoStatusChangeRequest(string status, string? reason = null);
}
