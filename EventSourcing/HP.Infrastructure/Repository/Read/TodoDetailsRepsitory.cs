using HP.Domain.Todos.Read;
using HP.Infrastructure.DbAccess;

namespace HP.Infrastructure.Repository.Read
{
    public class TodoDetailsRepsitory : BaseRepository<TodoDetails>, ITodoRepository
    {
        // How to setup mongo DB Collection.
        public TodoDetailsRepsitory(IMongoDbContext dbContext) : base(dbContext)
        {
        }
        public Task RemoveTodoDetails(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveTodoDetails(TodoDetails todo)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTodoDetails(TodoDetails todo)
        {
            throw new NotImplementedException();
        }
    }
}
