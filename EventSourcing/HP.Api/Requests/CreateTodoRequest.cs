using HP.Domain;
namespace HP.Api.Requests
{
    public record CreateTodoRequest(string Title, string TodoType, string Description, TodoStatus Status, string[] Tags = null);
}
