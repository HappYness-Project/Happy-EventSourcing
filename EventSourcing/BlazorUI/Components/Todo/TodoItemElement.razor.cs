using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Application.Queries.Todos;
using HP.Domain;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components.Todo
{
    public partial class TodoItemElement : ComponentBase
    {
        [Inject] public IMediator _mediator { get; set; }
        [Parameter] public TodoDetailsDto Todo { get; set; }
        public TodoItem SelectTodoItem { get; set; }
        public bool UpdateTodoItemDialogOpen { get; set; } = false;
        public string newTodoStatus { get; set; } = string.Empty;

        private async Task DeleteTodoSubItem(string todoId, string subTodoId)
        {
            bool isRemoved = await _mediator.Send(new DeleteTodoItemCommand(todoId, subTodoId));
            if (isRemoved)
                await LoadTodoData();
        }

        private async Task LoadTodoData()
        {
            Todo = await _mediator.Send(new GetTodoById(Todo.TodoId));
            StateHasChanged();
        }
        private void OpenUpdateTodoItemDialog(TodoDetailsDto todo, TodoItem todoItem)
        {
            SelectTodoItem = todoItem;
            UpdateTodoItemDialogOpen = true;
            StateHasChanged();
        }
        private void OnUpdateTodoItemDialogClose(bool accepted)
        {
            SelectTodoItem = null;
            UpdateTodoItemDialogOpen = false;
            StateHasChanged();
        }
        private async Task StatusSelected(ChangeEventArgs args)
        {
            newTodoStatus = args.Value as string;
        }
    }
}
