using HP.Application.Commands;
using HP.Application.Commands.Todo;
using HP.Application.DTOs;
using HP.Shared;
using HP.Shared.Contacts;
using MediatR;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace BlazorUI.Components.Todo
{
    public partial class DisplayTodoList : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ICurrentUserService CurrentUserService { get; set; }
        [Inject] public ITodoService _todoService { get; set; }
        public TodoDetailsDto SelectedTodo { get; set; }
        public Type DynamicComponentType { get; set; }
        public IEnumerable<TodoDetailsDto> TodoDetailsDtos { get; set; } = new List<TodoDetailsDto>();
        public Dictionary<string, object> DynamicComponentParams { get; set; }
        public bool DeleteTodoDialogOpen { get; set; } = false;
        public string _deleteTodoId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }
        public void OnClickViewDetails(string todoId)
        {
            NavigationManager.NavigateTo($"todos/details/{todoId}");
        }
        protected async Task OnDeleteDialogClose(bool accepted)
        {
            if (accepted)
            {
                var result = await _todoService.DeleteAsync(_deleteTodoId);
                _deleteTodoId = string.Empty;
                await LoadData();
            }
            DeleteTodoDialogOpen = false;
            StateHasChanged();
        }
        protected async void OpenDeleteDialog(string todoId)
        {
            DeleteTodoDialogOpen = true;
            _deleteTodoId = todoId;
        }
        private async Task LoadData()
        {
            var temp_username = CurrentUserService.CurrentUser.UserName;
            var result = await _todoService.GetTodosByPersonId(temp_username);
            if(result.IsSuccess)
            {
                TodoDetailsDtos = result.Data;
                StateHasChanged();
            }
        }
    }
}
