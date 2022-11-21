using HP.Application.DTOs;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components.Todo
{
    public partial class CompletedTodoItemList : ComponentBase
    {
        [Parameter] public TodoDetailsDto SelectedTodo { get; set; } = new();
        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
    }
}
