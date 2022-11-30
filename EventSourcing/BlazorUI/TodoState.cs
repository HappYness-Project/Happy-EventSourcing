using HP.Application.DTOs;
using HP.Domain;

namespace BlazorUI
{
    public interface ITodoState
    {

    }
    public class TodoState : ITodoState
    {
        public bool ShowingConfigureDialog { get; private set; }
        public TodoDetailsDto ConfiguringTodo { get; private set; }
        public event Action<int> OnTodoItemChanged;
        public TodoState()
        {

        }
        public void ShowConfigureTodoItemDialog()
        {
            ShowingConfigureDialog = true;
        }
        public void CancelConfigureTodoItemDialog()
        {
            ShowingConfigureDialog = false;
        }
        public void ConfirmConfigureTodoItemDialog()
        {
            ShowingConfigureDialog = false;
        }
        public void RemoveConfiguredTodoItem(TodoItem todoItem)
        {
            ConfiguringTodo.SubTodos.Remove(todoItem);
        }
    }
}
