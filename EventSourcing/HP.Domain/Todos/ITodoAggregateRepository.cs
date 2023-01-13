using HP.Core.Common;
namespace HP.Domain
{
    public interface ITodoAggregateRepository : IBaseRepository<Todo>
    {
        Task<Todo> GetActiveTodoById(Guid todoId);
        Task<IEnumerable<Todo>> GetListByPersonName(string personName);
        Task<IEnumerable<Todo>> GetListByTags(string[] tags);
        IEnumerable<Todo> Search(int page, int recordsPerPage, string TodoTitle, out int totalCount);
    }
}
