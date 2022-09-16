using HP.Domain;
namespace HP.Api.Requests
{
    public record CreateTodoRequest(string Title, string Description, string TodoType, string[] Tag);
}
