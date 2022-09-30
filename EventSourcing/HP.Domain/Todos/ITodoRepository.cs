namespace HP.Domain
{
    public interface ITodoRepository : IBaseRepository<Todo>
    {
        Task<Todo> GetActiveTodoById(string todoId);
        Task<IEnumerable<Todo>> GetListByUserId(string userId);
        Task<IEnumerable<Todo>> GetListByTags(string[] tags);
        IEnumerable<Todo> Search(int page, int recordsPerPage, string TodoTitle, out int totalCount);

    }
}
