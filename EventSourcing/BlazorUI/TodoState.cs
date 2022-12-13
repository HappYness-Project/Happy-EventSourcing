using HP.Application.DTOs;
using HP.Domain;

namespace BlazorUI
{
    public class TodoState 
    {
        public TodoDetailsDto Todo { get; private set; } = new TodoDetailsDto();
        //public event Action<int> OnTodoItemChanged
        public event Action OnStateChange;
        public void SetValue(TodoDetailsDto value)
        {
            Todo = value;
            NotifyStateChanged();
        }
        public void AddTodoItem(TodoItemDto todoItem)
        {
            Todo.SubTodos.Add(todoItem);
            NotifyStateChanged();
        }
        public void RemoveTodoItem(TodoItemDto todoItem)
        {
            Todo.SubTodos.Remove(todoItem);
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
