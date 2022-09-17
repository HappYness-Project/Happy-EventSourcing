namespace HP.Api.Requests
{
    public record CreateTodoItemRequest(string TodoTitle, string TodoType, string Description, string[] Tags = null);
}
