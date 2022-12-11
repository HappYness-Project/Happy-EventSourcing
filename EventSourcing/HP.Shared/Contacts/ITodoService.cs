using HP.Application.DTOs;
using HP.Core.Commands;
using HP.Shared.Common;
using HP.Shared.Requests.Todos;
namespace HP.Shared.Contacts
{
    public interface ITodoService
    {
        Task<Result<IEnumerable<TodoDetailsDto>>> GetTodos();
        Task<Result<TodoDetailsDto>> GetTodoById(string todoId);
        Task<CommandResult> CreateAsync(CreateTodoDto request);
        Task<CommandResult> UpdateAsync(UpdateTodoDto request);
        Task<CommandResult> CreateTodoItemAsync(string todoId, CreateTodoItemDto request);
        Task<CommandResult> UpdateTodoItemAsync(string todoId, TodoItemDto todoItem);
        Task<CommandResult> DeleteTodoItemAsync(string todoId, string todoItemId);
        Task<Result<CommandResult>> DeleteAsync(string todoId);
        Task<Result<CommandResult>> UpdateTodoStatus(string todoId, string status);
        Task<CommandResult> UpdateTodoItemStatus(string todoId, string todoItemId, string status);
        Task<IEnumerable<TodoItemDto>> GetTodoItemsById(string todoId);
        Task<IEnumerable<TodoItemDto>> GetTodoItemsByStatus(string todoId, string status);
        Task<Result<int>> GetTodoItemsCount(bool onlyActive = true);
        Task<Result<IEnumerable<TodoDetailsDto>>> GetTodosByPersonId(string temp_username);
    }
}
