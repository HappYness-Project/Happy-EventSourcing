using HP.Application.DTOs;
using HP.Shared.Contacts;
using HP.Shared.Requests.Todos;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components.Todo
{
    public partial class DisplayTodoItemList : ComponentBase
    {
        [Inject] private ITodoService _todoService { get; set; }
        public TodoItemDto SelectTodoItem { get; set; }
        public bool UpdateTodoItemDialogOpen { get; set; } = false;
        protected override void OnInitialized()
        {
            _todoService.TodoChanged += StateHasChanged;
        }
        public void Dispose()
        {
            _todoService.TodoChanged -= StateHasChanged;
        }
        private void OnUpdateTodoItemDialogClose(bool accepted)
        {
            SelectTodoItem = null;
            UpdateTodoItemDialogOpen = false;
            StateHasChanged();
        }

        private async Task DeleteTodoSubItem(string subTodoId)
        {
            var result = await _todoService.DeleteTodoItemAsync(subTodoId);
            if (result.IsSuccess)
                await LoadTodoData();
        }
        private async Task StatusCompleteTodoItem()
        {
            await LoadTodoData();
        }
        private async Task LoadTodoData()
        {
            var result = await _todoService.GetTodoById(_todoService.Todo.TodoId);
            if (result.IsSuccess)
            {
                _todoService.Todo = result.Data;
                _todoService.CompletedTodoItems = await _todoService.GetTodoItemsByStatus("complete");
                StateHasChanged();
            }
        }
        private async Task ChangeTodoItemStatus(string todoItemId)
        {
            var updateTodo = await _todoService.GetTodoById();
            if (updateTodo.IsSuccess)
                _todoService.Todo = updateTodo.Data;

            _todoService.CompletedTodoItems = await _todoService.GetTodoItemsByStatus("complete");
            StateHasChanged();
        }
    }
}
