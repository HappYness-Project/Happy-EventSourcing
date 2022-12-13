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
        [Parameter] public string ParentTodoId { get; set; }
        public TodoItemDto SelectTodoItem { get; set; }
        public IEnumerable<TodoItemDto> CompletedTodoItems { get; set; }
        public bool UpdateTodoItemDialogOpen { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            CompletedTodoItems = await _todoService.GetTodoItemsByStatus(ParentTodoId, "complete");
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
            var result = await _todoService.DeleteTodoItemAsync(ParentTodoId, subTodoId);
            if (result.IsSuccess)
            {
                CompletedTodoItems = await _todoService.GetTodoItemsByStatus(ParentTodoId, "complete");
            }

        }
    }
}