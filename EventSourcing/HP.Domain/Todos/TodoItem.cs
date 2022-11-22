using HP.Domain.Common;
namespace HP.Domain
{
    public class TodoItem : Entity
    {
        public string Title { get; set; }
        public string TodoType {get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDone { get; set; }
        public TodoStatus TodoStatus { get; set; }
        public DateTime Completed { get; set; }

        public TodoItem(string title, string todoType, string desc)
        {
            Title = title;
            TodoType = todoType;
            Description = desc;
            IsActive = true;
            IsDone = false;
            TodoStatus = TodoStatus.NotDefined;
        }

        public void MarkCompleted()
        {
            this.IsDone = true;
            this.Completed = DateTime.Now;
            TodoStatus = TodoStatus.Complete;
        }
        protected override void When(IDomainEvent @event) 
        {
            throw new NotImplementedException();
        }
    }
}
