using HP.Application.DTOs;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components.Todo
{
    public partial class TodoElement : ComponentBase
    {
        [Parameter] public TodoDetailsDto Todo { get; set; } = new();
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        private void OnClickTodoElement(string todoId)
        {
            var clickedTodo = todoId;
        }
    }
}
