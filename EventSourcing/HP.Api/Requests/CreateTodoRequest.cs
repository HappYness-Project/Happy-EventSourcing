namespace HP.Api.Requests
{
    public record CreateTodoRequest(string Title, string TodoType, string Description, string[] Tags = null);
}
