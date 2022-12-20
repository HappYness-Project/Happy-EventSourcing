using HP.Application.DTOs;
using HP.Core.Commands;
using HP.Shared.Common;
using HP.Shared.Contacts;
using HP.Shared.Requests.Todos;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Pages
{
    public partial class TodoDetails : ComponentBase
    {
        [Inject] public ITodoService _todoService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Parameter] public string TodoId { get; set; } = string.Empty;
        public bool DeleteTodoDialogOpen { get; set; }
        public bool AddTodoItemDialogOpen { get; set; }
        private string mainUserName { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var todo = await _todoService.GetTodoById(TodoId);
            await _todoService.SetValue(todo.Data);
            _todoService.TodoChanged += StateHasChanged;
        }

        protected override async Task OnParametersSetAsync()
        {
            await LoadTodoData();
        }
        public void Dispose()
        {
            _todoService.TodoChanged -= StateHasChanged;
        }
        protected async Task SaveTodoChanges()
        {
            UpdateTodoDto request = new UpdateTodoDto()
            {
                Id = _todoService.Todo.TodoId,
                TodoTitle = _todoService.Todo.TodoTitle,
                TodoType = _todoService.Todo.TodoType,
                Description = _todoService.Todo.Description,
                TargetStartDate = _todoService.Todo.TargetStartDate,
                TargetEndDate = _todoService.Todo.TargetEndDate,
            };
            var result = await _todoService.UpdateAsync(request);
            if (result.IsSuccess)
                await LoadTodoData();
        }
        private async Task LoadTodoData()
        {
            var result = await _todoService.GetTodoById(TodoId);
            if (result.IsSuccess)
            {
                _todoService.Todo = result.Data;
                StateHasChanged();
            }
        }
        
        private async Task<Result<CommandResult>> PerformStatusOperation(string command) => command switch
        {
            "start" =>    await _todoService.UpdateTodoStatus(_todoService.Todo.TodoId, "start"),
            "stop" =>     await _todoService.UpdateTodoStatus(_todoService.Todo.TodoId, "stop"),
            "accept" =>   await _todoService.UpdateTodoStatus(_todoService.Todo.TodoId,"accept"),
            "pending" =>  await _todoService.UpdateTodoStatus(_todoService.Todo.TodoId, "pending"),
            "complete" => await _todoService.UpdateTodoStatus(_todoService.Todo.TodoId, "complete"),
            _ => throw new ArgumentException("Invalid string value for command", nameof(command)),
        };
        private async Task StatusSelected(string value)
        {
            var result = await PerformStatusOperation(value);
            await LoadTodoData();
        }
        private async Task OnAddTodoItemDialogClose(bool accepted)
        {
            AddTodoItemDialogOpen = false;
            if (accepted)
            {
                await LoadTodoData();
            }
        }
        private async void OnDeleteDialogClose(bool accepted)
        {
            if (accepted)
            {
                await _todoService.DeleteAsync(_todoService.Todo.TodoId);
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
