namespace HP.Api.Requests
{
    public record UpdateTodoRequest(string TodoId, string Title, string Type, string Description, string[] Tags);
}
