using BlazorUI.Services.Todo;
using HP.Application.Commands;
using HP.Application.Commands.Todo;
using HP.Application.DTOs;
using HP.Application.Queries.Todos;
using HP.Shared.Contacts;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components.Todo
{
    public partial class CompletedTodoItemList : ComponentBase
    {
        [Inject] public ITodoService _todoService { get; set; }
        [CascadingParameter(Name = "ParentTodoDto")]
        public TodoDetailsDto Todo { get; set; }
        public TodoItemDto SelectTodoItem { get; set; }
        public IEnumerable<TodoItemDto> CompletedTodoItems { get; set; }
        public bool UpdateTodoItemDialogOpen { get; set; } = false;
        protected override async void OnInitialized()
        {
            await LoadCompletedTodoItemData();
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
            if(result.IsSuccess)
                await LoadTodoData();
        }
        private async Task LoadTodoData()
        {
            var result = await _todoService.GetTodoById(Todo.TodoId);
            if (!result.IsSuccess)
            {
                Todo = result.Data;
                StateHasChanged();
            }
        }
        private async Task LoadCompletedTodoItemData()
        {
            if(Todo.TodoId != null)
            {
                CompletedTodoItems = await _todoService.GetTodoItemsByStatus(Todo.TodoId, "complete");
                StateHasChanged();
            }
        }
    }
}