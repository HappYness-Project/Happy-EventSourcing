using HP.Application.DTOs;
using HP.Core.Commands;
using HP.Shared.Common;
using HP.Shared.Requests.Todos;
namespace HP.Shared.Contacts
{
    public interface ITodoService
    {
        event Action TodoChanged;
        Task SetValue(TodoDetailsDto newTodo);
        TodoDetailsDto Todo { get; set; }
        IEnumerable<TodoItemDto> CompletedTodoItems { get; set; }
        Task<Result<TodoDetailsDto>> GetTodoById(string? todoId = null);
        Task<CommandResult> CreateAsync(CreateTodoDto request);
        Task<CommandResult> UpdateAsync(UpdateTodoDto request);
        Task<CommandResult> CreateTodoItemAsync(string todoId, CreateTodoItemDto request);
        Task<CommandResult> UpdateTodoItemAsync(string todoId, TodoItemDto todoItem);
        Task<CommandResult> DeleteTodoItemAsync(string todoId, string todoItemId);
        Task<CommandResult> ToggleActive(string todoId, bool activate);
        Task<CommandResult> ToggleTodoItemActive(string todoId, string todoItemId, bool activate);
        Task<Result<CommandResult>> DeleteAsync(string todoId);
        Task<Result<CommandResult>> UpdateTodoStatus(string todoId, string status);
        Task<CommandResult> UpdateTodoItemStatus(string todoId, string todoItemId, string status);
        Task<IEnumerable<TodoItemDto>> GetTodoItemsById(string todoId);
        Task<IEnumerable<TodoItemDto>> GetTodoItemsByStatus(string todoId, string status);
        Task<Result<int>> GetTodoItemsCount(bool onlyActive = true);
        Task<Result<IEnumerable<TodoDetailsDto>>> GetTodosByPersonId(string temp_username);
    }
}
