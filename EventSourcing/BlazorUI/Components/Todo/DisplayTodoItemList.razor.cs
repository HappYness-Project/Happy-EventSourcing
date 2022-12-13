using HP.Application.DTOs;
using HP.Shared.Contacts;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components.Todo
{
    public partial class DisplayTodoItemList : ComponentBase
    {
        [Inject] private ITodoService _todoService { get; set; }
        
        [CascadingParameter(Name = "ParentTodoDto")] 
        public TodoDetailsDto Todo { get; set; }
        public TodoItemDto SelectTodoItem { get; set; }
        public bool UpdateTodoItemDialogOpen { get; set; } = false;
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        private void OnUpdateTodoItemDialogClose(bool accepted)
        {
            SelectTodoItem = null;
            UpdateTodoItemDialogOpen = false;
            StateHasChanged();
        }

        private async Task DeleteTodoSubItem(string subTodoId)
        {
            var result = await _todoService.DeleteTodoItemAsync(Todo.TodoId, subTodoId);
            if (result.IsSuccess)
                await LoadTodoData();
        }
        private async Task StatusCompleteTodoItem()
        {
            await LoadTodoData();
        }
        private async Task LoadTodoData()
        {
            var result = await _todoService.GetTodoById(Todo.TodoId);
            if (result.IsSuccess)
            {
                Todo = result.Data;
                StateHasChanged();
            }
        }
    }
}
