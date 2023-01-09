using HP.Core.Models;
namespace HP.Domain
{
    public class TodoItem 
    {
        public Guid Id { get; set; }
        public string Title { get; private set; }
        public string TodoType {get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; set; }
        public bool IsDone { get; set; }
        public TodoStatus TodoStatus { get; set; }
        public DateTime Completed { get; set; }

        public TodoItem()
        {
            IsDone = false;
            TodoStatus = TodoStatus.NotDefined;
        }
        public TodoItem(string title, string todoType, string desc)
        {
            Id = Guid.NewGuid();
            Title = title;
            TodoType = todoType;
            Description = desc;
            IsActive = true;
            IsDone = false;
            TodoStatus = TodoStatus.NotDefined;
        }
        public void Update(string title, string todoType, string desc)
        {
            this.Title = title;
            this.TodoType = todoType;
            this.Description = desc;
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
    }
}
