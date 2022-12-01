using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Application.Queries.Todos;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components.Todo
{
    public partial class CompletedTodoItemList : ComponentBase
    {
        [Inject] public IMediator _mediator { get; set; }
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
            bool isRemoved = await _mediator.Send(new DeleteTodoItemCommand(Todo.TodoId, subTodoId));
            if (isRemoved)
                await LoadTodoData();
        }
        private async Task LoadTodoData()
        {
            Todo = await _mediator.Send(new GetTodoById(Todo.TodoId));
            StateHasChanged();
        }
        private async Task LoadCompletedTodoItemData()
        {
            if(Todo.TodoId != null)
            {
                CompletedTodoItems = await _mediator.Send(new GetCompletedTodoItemsByTodoId(Todo.TodoId));
                StateHasChanged();
            }
        }
    }
}