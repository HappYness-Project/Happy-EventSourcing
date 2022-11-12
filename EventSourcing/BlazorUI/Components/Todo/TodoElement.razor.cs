using HP.Application.DTOs;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components.Todo
{
    public partial class TodoElement : ComponentBase
    {
        [Parameter]
        public TodoBasicInfoDto TodoBasicInfoDto { get; set; }
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
