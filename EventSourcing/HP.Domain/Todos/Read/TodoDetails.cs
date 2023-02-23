using HP.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace HP.Domain.Todos.Read
{
    [Table("TodoDetails")]
    public class TodoDetails : BaseEntity, IReadModel
    {
        [SetsRequiredMembers]
        public TodoDetails(Guid Id)
        {
            this.Id = Id;
            Score = 0;
            IsDone = false;
            IsActive = true;
            SubTodos = null;
            Tags = new string[] { };
            TodoStatus = Domain.TodoStatus.NotDefined.ToString();
        }
        public required Guid PersonId { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public string TodoType { get; set; }
        public string[] Tags { get; set; }
        public double Score { get; set; }
        public bool IsDone { get; set; }
        public bool IsActive { get; set; }
        public ICollection<TodoItem> SubTodos { get; set; }
        public string TodoStatus { get; set; }
        public DateTime TargetStartDate { get; set; }
        public DateTime TargetEndDate { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
        public DateTime Completed { get; set; }
    }
}
