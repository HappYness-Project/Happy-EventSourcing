using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Application.Queries.Todos;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Pages
{
    public partial class TodoDetails : ComponentBase
    {
        [Inject] public IMediator _mediator { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Parameter] public string TodoId { get; set; } = string.Empty;
        [Parameter] public EventCallback OnSubmitCallback { get; set; }
        public TodoDetailsDto Todo { get; set; } = new();
        public string newTodoStatus { get; set; }
        public bool DeleteTodoDialogOpen { get; set; }
        public bool AddTodoItemDialogOpen { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadTodoData();
            if (Todo.TodoStatus != null)
                newTodoStatus = Todo.TodoStatus.Name;
        }
        protected async Task SaveTodoChanges()
        {
            bool isUpdated = await _mediator.Send(new UpdateTodoCommand(Todo.TodoId, Todo.TodoTitle, Todo.TodoType, Todo.Description, null));
            if (isUpdated)
                await LoadTodoData();
        }
        private async Task LoadTodoData()
        {
            Todo = await _mediator.Send(new GetTodoById(TodoId));
            StateHasChanged();
        }
        private async Task<MediatR.Unit> PerformStatusOperation(string command) => command switch
        {
            "start" => await _mediator.Send(new StartTodoCommand(Todo.TodoId)),
            "stop" => await _mediator.Send(new StopTodoCommand(Todo.TodoId, "Reason needs to be updated.")),
            "accept" => await _mediator.Send(new AcceptTodoCommand(Todo.TodoId)),
            "pending" => await _mediator.Send(new PendingTodoCommand(Todo.TodoId)),
            "complete" => await _mediator.Send(new CompleteTodoCommand(Todo.TodoId)),
            _ => throw new ArgumentException("Invalid string value for command", nameof(command)),
        };
        private void StatusSelected(ChangeEventArgs args)
        {
            newTodoStatus = args.Value as string;
            PerformStatusOperation(newTodoStatus);
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
                await _mediator.Send(new DeleteTodoCommand(Todo.TodoId));
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
    }
}
