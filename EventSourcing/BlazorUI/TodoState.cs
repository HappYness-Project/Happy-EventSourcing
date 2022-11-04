using HP.Application.DTOs;
using HP.Domain;

namespace BlazorUI
{
    public class TodoState
    {
        public bool ShowingConfigureDialog { get; private set; }
        public TodoItem ConfiguringTodoItem { get; private set; }
        public TodoDetailsDto Todo { get; private set; } = new TodoDetailsDto();
        public void ShowConfigureTodoItemDialog(TodoItem todoItem)
        {
            ConfiguringTodoItem = todoItem;
            ShowingConfigureDialog = true;
        }
        public void CancelConfigureTodoItemDialog()
        {
            ConfiguringTodoItem = null;
            ShowingConfigureDialog = false;
        }
        public void ConfirmConfigureTodoItemDialog()
        {
            Todo.SubTodos.Add(ConfiguringTodoItem);
            ConfiguringTodoItem = null;
            ShowingConfigureDialog = false;
        }
        public void RemoveConfiguredTodoItem(TodoItem todoItem)
        {
            Todo.SubTodos.Remove(todoItem);
        }
    }
}
