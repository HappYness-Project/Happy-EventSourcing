namespace HP.Api.Requests
{
    public record UpdateTodoRequest(string TodoId, string TodoTotle, string TodoDescription, string[] Tags);
}
