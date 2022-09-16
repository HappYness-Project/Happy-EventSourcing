using HP.Application.Commands;
using HP.Application.DTOs;

namespace HP.Application.Handlers
{
<<<<<<< HEAD
    public record CreateTodoCommand(string UserName, string TodoTitle, string TodoType, string Description = null,  string[] Tag = null) : CommandBase<TodoDetailsDto>;
=======
    public record CreateTodoCommand(string UserId, string todoTitle, string todoType, string Description = null,  string[] Tag = null) : CommandBase<TodoDetailsDto>;
>>>>>>> 0bc70ef680e150468d640d093d814c585a5f02d0
}
