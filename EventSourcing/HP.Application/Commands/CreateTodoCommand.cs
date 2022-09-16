using HP.Application.Commands;
using HP.Application.DTOs;

namespace HP.Application.Handlers
{
    public record CreateTodoCommand(string UserId, string TodoTitle, string TodoType, string Description = null,  string[] Tag = null) : CommandBase<TodoDetailsDto>;
}
