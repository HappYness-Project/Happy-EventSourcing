namespace HP.Domain.Todos.Read
{
    public interface ITodoRepository
    {
        Task SaveTodoDetails(TodoDetails todo);
        Task UpdateTodoDetails(TodoDetails todo);
        Task RemoveTodoDetails(Guid id);
    }
}
