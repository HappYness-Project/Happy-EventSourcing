using HP.Application.DTOs;
using HP.Core.Commands;
using HP.Shared.Common;
using HP.Shared.Requests.Todos;

namespace HP.Shared.Contacts
{
    public interface ITodoService
    {
        Task<Result<IEnumerable<TodoDetailsDto>>> GetTodos();
        Task<Result<CommandResult>> CreateAsync(CreateTodoRequest request);
        Task<Result<CommandResult>> UpdateAsync(UpdateTodoRequest request);
        Task<Result<TodoDetailsDto>> GetTodoDetails(string TodoId);
        Task<Result<int>> GetTodoItemsCount(bool OnlyActive = true);
        Task<Result<IEnumerable<TodoDetailsDto>>> GetTodosByPersonId(string temp_username);
    }
}
