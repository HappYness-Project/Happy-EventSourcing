using HP.Domain.Todos;

namespace HP.Api.DTO
{
    public record CreateTodoDto(string Title, string Description, TodoStatus Status, string[] Tag);
}
