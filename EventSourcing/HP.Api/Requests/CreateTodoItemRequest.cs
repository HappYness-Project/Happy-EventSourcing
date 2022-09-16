using HP.Domain;
namespace HP.Api.Requests
{
    public record CreateTodoItemRequest(string Title, string TodoType, string Description, TodoStatus Status, string[] Tags = null);
}
