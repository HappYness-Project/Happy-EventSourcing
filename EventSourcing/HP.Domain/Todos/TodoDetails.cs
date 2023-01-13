using HP.Core.Models;
namespace HP.Domain.Todos
{
    public class TodoDetails : BaseEntity, IReadModel
    {
        public TodoDetails(Guid Id) {
            this.Id = Id;
        }
        public string PersonId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public string TodoType { get; set; }
        public string[] Tags { get; set; }
        public double Score { get; set; }
        public ICollection<TodoItem> SubTodos { get; set; }
        public string TodoStatus { get; set; }
        public DateTime TargetStartDate { get; set; }
        public DateTime TargetEndDate { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
        public DateTime Completed { get; set; }
    }
}
