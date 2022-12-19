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
        public TodoItemDto SelectTodoItem { get; set; }
        [Parameter] public Action<string> OnChange { get; set; }
        public bool UpdateTodoItemDialogOpen { get; set; } = false;
        private string _parentTodoId;
        protected override async Task OnInitializedAsync()
        {
            _todoService.TodoChanged += StateHasChanged;
        }
        public void Dispose()
        {
            _todoService.TodoChanged -= StateHasChanged;
        }
        protected override async Task OnParametersSetAsync()
        {
            _parentTodoId = _todoService.Todo.TodoId;
            _todoService.CompletedTodoItems = await _todoService.GetTodoItemsByStatus(_parentTodoId, "complete");
        }

        private async void OnUpdateTodoItemDialogClose(bool accepted)
        {
            SelectTodoItem = null;
            UpdateTodoItemDialogOpen = false;
            StateHasChanged();
        }
        private async Task DeleteTodoSubItem(string subTodoId)
        {
            var result = await _todoService.DeleteTodoItemAsync(_parentTodoId, subTodoId);
            if (result.IsSuccess)
            {
                _todoService.CompletedTodoItems = await _todoService.GetTodoItemsByStatus(_parentTodoId, "complete");
                _todoService.TodoChanged += StateHasChanged;
            }
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {

            }
        }

    }
}