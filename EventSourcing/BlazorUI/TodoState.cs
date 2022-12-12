using HP.Application.DTOs;

namespace BlazorUI
{
    public class TodoState 
    {
        public bool ShowingConfigureDialog { get; private set; }
        public TodoDetailsDto Todo { get; private set; } = new TodoDetailsDto();
        //public event Action<int> OnTodoItemChanged;
        public event Action OnStateChange;
        public void SetValue(TodoDetailsDto value)
        {
            Todo = value;
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnStateChange?.Invoke();
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
    }
}
