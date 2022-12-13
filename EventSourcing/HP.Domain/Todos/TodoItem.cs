using HP.Core.Models;
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

        public TodoItem(){}
        public TodoItem(string title, string todoType, string desc)
        {
            Title = title;
            TodoType = todoType;
            Description = desc;
            IsActive = true;
            IsDone = false;
            TodoStatus = TodoStatus.NotDefined;
        }

        private void MarkCompleted()
        {
            this.IsDone = true;
            this.Completed = DateTime.Now;
            this.TodoStatus = TodoStatus.Complete;
        }
        public void SetStatus(string status)
        {
            switch(status)
            {
                case "pending":
                    this.TodoStatus = TodoStatus.Pending;
                    this.IsDone = false;
                    break;

                case "accept":
                    this.TodoStatus = TodoStatus.Accept;
                    this.IsDone = false;
                    break;
                case "start":
                    this.TodoStatus = TodoStatus.Start;
                    this.IsDone = false;
                    break;
                case "complete":
                    MarkCompleted();
                    break;
                case "stop":
                    this.TodoStatus = TodoStatus.Stop;
                    break;
            }
        }
        protected override void When(IDomainEvent @event) 
        {
            throw new NotImplementedException();
        }
    }
}
