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
        [Parameter] public TodoItemDto TodoItem { get; set; }
        [CascadingParameter(Name = "ParentTodoDto")]
        [Parameter] public TodoDetailsDto ParentTodo { get; set; }
        [Parameter] public EventCallback<string> TodoItemRemoved { get; set; }
        [Parameter] public EventCallback<string> ItemMarkedCompleted { get; set; }
        
        public TodoItemDto SelectTodoItem { get; set; }
        public bool UpdateTodoItemDialogOpen { get; set; } = false;

        protected async Task CheckBoxChanged(ChangeEventArgs e)
        {
            var value = (bool)e.Value;
            if (!value)
                await _mediator.Send(new DeactivateTodoItemCommand(ParentTodo.TodoId, TodoItem.Id));
            else
                await _mediator.Send(new ActivateTodoItemCommand(ParentTodo.TodoId, TodoItem.Id));
        }
        protected async Task DeleteButtonClicked(string removeTodoItemId)
        {
            await TodoItemRemoved.InvokeAsync(removeTodoItemId);
        }

        private void OpenUpdateTodoItemDialog(TodoItemDto todoItem)
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

            var IsUpdated = await _mediator.Send(new UpdateStatusTodoItemCommand(ParentTodo.TodoId, TodoItem.Id, args.Value as string));
            if (IsUpdated && (string)args.Value == "complete")
                await ItemMarkedCompleted.InvokeAsync(TodoItem.Id);

            StateHasChanged();
        }

    }
}
