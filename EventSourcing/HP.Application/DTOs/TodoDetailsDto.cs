using HP.Domain.Todos;
namespace HP.Application.DTOs
{
    public record TodoDetailsDto(string UserId, string TodoId, string TodoTitle, string TodoType, TodoStatus TodoStatus);
}
