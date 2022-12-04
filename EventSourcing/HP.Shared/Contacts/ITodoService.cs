using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Core.Commands;
using HP.Shared.Requests.Todos;

namespace HP.Shared.Contacts
{
    public interface ITodoService
    {
        Task<Result<IEnumerable<TodoDetailsDto>>> GetTodos();
        Task<Result<TodoDetailsDto>> GetTodoDetails(string TodoId);
        Task<Result<int>> GetTodoItemsCount(bool OnlyActive = true);
        Task<Result<CommandResult>> CreateAsync(CreateTodoItemModel createTodoItemModel);
    }
}
