using HP.Application.Commands.Todo;
using HP.Application.DTOs;
using HP.Core.Commands;
using HP.Shared.Common;
using HP.Shared.Contacts;
using HP.Shared.Requests.Todos;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Pages
{
    public partial class TodoDetails : ComponentBase
    {
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
            UpdateTodoDto request = new UpdateTodoDto()
            {
                TodoId = SelectedTodo.TodoId,
                TodoTitle = SelectedTodo.TodoTitle,
                TodoType = SelectedTodo.TodoType,
                Description = SelectedTodo.Description,
                Tags = null,
                TargetStartDate = SelectedTodo.TargetStartDate,
                TargetEndDate = SelectedTodo.TargetEndDate,
            };
            var result = await _todoService.UpdateAsync(request);
            if (result.IsSuccess)
                await LoadTodoData();
        }
        private async Task LoadTodoData()
        {
            var check = await _todoService.GetTodoDetails(TodoId);
            if (check.IsSuccess)
                SelectedTodo = check.Data;
            StateHasChanged();
        }
        private async Task<Result<CommandResult>> PerformStatusOperation(string command) => command switch
        {
            "start" =>    await _todoService.UpdateTodoStatus(SelectedTodo.TodoId, "start"),
            "stop" => await _todoService.UpdateTodoStatus(SelectedTodo.TodoId, "stop"),
            "accept" =>   await _todoService.UpdateTodoStatus(SelectedTodo.TodoId,"accept"),
            "pending" =>  await _todoService.UpdateTodoStatus(SelectedTodo.TodoId, "pending"),
            "complete" => await _todoService.UpdateTodoStatus(SelectedTodo.TodoId, "complete"),
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
                await _todoService.DeleteAsync(SelectedTodo.TodoId);
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
