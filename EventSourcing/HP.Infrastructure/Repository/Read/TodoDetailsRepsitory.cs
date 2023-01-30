using HP.Domain.Todos.Read;
using HP.Infrastructure.DbAccess;
namespace HP.Infrastructure.Repository.Read
{
    public class TodoDetailsRepsitory : BaseRepository<TodoDetails>, ITodoDetailsRepository
    {
        // How to setup mongo DB Collection.
        public TodoDetailsRepsitory(HpReadDbContext dbContext) : base(dbContext) { }
    }
}
