using HP.Domain.Common;
namespace HP.Domain
{
    public class TodoItem : Entity
    {
        public string Title { get; set; }
        public string TodoType {get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public TodoStatus TodoStatus { get; set; }

        public TodoItem(string title, string todoType, string desc)
        {
            Title = title;
            TodoType = todoType;
            Description = desc;
            IsActive = true;
            TodoStatus = TodoStatus.NotDefined;
        }
        protected override void When(IDomainEvent @event) 
        {
            throw new NotImplementedException();
        }
    }
}
