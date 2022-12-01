using HP.Application.Commands;
using HP.Application.Commands.Todo;
using HP.Application.DTOs;
using HP.Application.Queries.Todos;
using HP.Core.Commands;
using HP.Shared.Contacts;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Pages
{
    public partial class TodoDetails : ComponentBase
    {
        [Inject] public IMediator _mediator { get; set; }
        [Inject] public ITodoService _todoService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Parameter] public string TodoId { get; set; } = string.Empty;
        public TodoDetailsDto SelectedTodo { get; set; } = new();
        public bool DeleteTodoDialogOpen { get; set; }
        public bool AddTodoItemDialogOpen { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadTodoData();
        }
        protected async Task SaveTodoChanges()
        {
            var result = await _mediator.Send(new UpdateTodoCommand(SelectedTodo.TodoId, SelectedTodo.TodoTitle, SelectedTodo.TodoType, SelectedTodo.Description, null, SelectedTodo.TargetStartDate, SelectedTodo.TargetEndDate));
            if (result.IsSuccess)
                await LoadTodoData();
        }
        private async Task LoadTodoData()
        {
            var check = await _todoService.GetTodoDetails(TodoId);
            if (check.IsSuccess)
            {
                SelectedTodo = check.Data;
                StateHasChanged();
            }
        }
        private async Task<CommandResult> PerformStatusOperation(string command) => command switch
        {
            "start" =>    await _mediator.Send(new StartTodoCommand(SelectedTodo.TodoId)),
            "stop" =>     await _mediator.Send(new StopTodoCommand(SelectedTodo.TodoId, "Reason needs to be updated.")),
            "accept" =>   await _mediator.Send(new AcceptTodoCommand(SelectedTodo.TodoId)),
            "pending" =>  await _mediator.Send(new PendingTodoCommand(SelectedTodo.TodoId)),
            "complete" => await _mediator.Send(new CompleteTodoCommand(SelectedTodo.TodoId)),
            _ => throw new ArgumentException("Invalid string value for command", nameof(command)),
        };
        private async Task StatusSelected(string value)
        {
            await PerformStatusOperation(value);
            await LoadTodoData();
        }
        private async Task OnAddTodoItemDialogClose(bool accepted)
        {
            AddTodoItemDialogOpen = false;
            if (accepted)
                await LoadTodoData();
        }
        private async void OnDeleteDialogClose(bool accepted)
        {
            if (accepted)
            {
                await _mediator.Send(new DeleteTodoCommand(SelectedTodo.TodoId));
                NavigationManager.NavigateTo("todos");
            }
            DeleteTodoDialogOpen = false;
            StateHasChanged();
        }
        private void OpenDeleteDialog()
        {
            DeleteTodoDialogOpen = true;
            StateHasChanged();
        }
        private void OpenAddTodoItemDialog(TodoDetailsDto todo)
        {
            AddTodoItemDialogOpen = true;
            StateHasChanged();
        }
        private async Task UpdateLocationOfItems(string itemId)
        {

        }
    }
}
