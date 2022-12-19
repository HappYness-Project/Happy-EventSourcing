using BlazorUI.Services.Todo;
using HP.Application.Commands;
using HP.Application.Commands.Todo;
using HP.Application.DTOs;
using HP.Shared.Contacts;
using MediatR;
using Microsoft.AspNetCore.Components;
namespace BlazorUI.Components.Todo
{
    public partial class TodoItemElement : ComponentBase
    {
        [Inject] public ITodoService _todoService { get; set; }
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
            var result = await _todoService.ToggleTodoItemActive(ParentTodo.TodoId, TodoItem.Id, value);
        }
        protected async Task DeleteButtonClicked(string removeTodoItemId)
        {
            await TodoItemRemoved.InvokeAsync(removeTodoItemId);
        }
        protected async Task TodoItemStatusUpdated(string todoItemId)
        {
            
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

            var result = await _todoService.UpdateTodoItemStatus(ParentTodo.TodoId, TodoItem.Id, args.Value as string);
            if(!result.IsSuccess)
                return;

            if ((string)args.Value == "complete")
            {
                await ItemMarkedCompleted.InvokeAsync(TodoItem.Id);
                _todoService.CompletedTodoItems = await _todoService.GetTodoItemsByStatus(ParentTodo.TodoId, (string)args.Value);
                await InvokeAsync(StateHasChanged);
            }
            else
            {
                //
            }
        }

    }
}
