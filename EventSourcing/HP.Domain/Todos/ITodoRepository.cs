using HP.Domain.Todos;

namespace HP.Domain
{
    public interface ITodoRepository
    {
        Task SaveTodoDetails(TodoDetails todo);
        Task UpdateTodoDetails(TodoDetails todo);
        Task RemoveTodoDetails(Guid id);
    }
}
