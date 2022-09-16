using HP.Domain.Common;
namespace HP.Domain
{
    public class TodoItem : Entity
    {
        public string Title { get; private set; }
        public string TodoType {get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public TodoStatus TodoStatus { get; private set; }

        public TodoItem(string title, string todoType, string desc)
        {
            Title = title;
            TodoType = todoType;
            Description = desc;
            IsActive = true;
            TodoStatus = TodoStatus.Pending;
        }
        protected override void When(IDomainEvent @event) 
        {
            throw new NotImplementedException();
        }
    }
}
