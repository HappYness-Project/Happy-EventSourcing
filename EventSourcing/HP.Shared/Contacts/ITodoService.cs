using HP.Application.DTOs;
namespace HP.Shared.Contacts
{
    public interface ITodoService
    {
        Task<ServiceResult<TodoDetailsDto>> GetTodoDetails(string TodoId);
        Task<ServiceResult<int>> GetTodoItemsCount(bool OnlyActive = true);
    }
}
