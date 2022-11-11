using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Application.Queries.Todos;
using HP.Shared.Contacts;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components.Todo
{
    public partial class SearchTodo : ComponentBase
    {
        [Inject] public IMediator _mediator { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] private ICurrentUserService CurrentUserService { get; set; }
        [Parameter] public TodoDetailsDto SearchedTodo { get; set; }
        public bool DeleteDialogOpen { get; set; }
        private TodoDetailsDto _todoToDelete;
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }
        protected void OnClickViewDetails(string todoId)
        {
            NavigationManager.NavigateTo($"todos/details/{todoId}");
        }
        protected async void OpenDeleteDialog(TodoDetailsDto todo)
        {
            DeleteDialogOpen = true;
            _todoToDelete = todo;
            await LoadData();
        }
        protected async Task OnDeleteDialogClose(bool accepted)
        {
            if (accepted)
            {
                await _mediator.Send(new DeleteTodoCommand(_todoToDelete.TodoId));
                _todoToDelete = null;
            }
            DeleteDialogOpen = false;
            StateHasChanged();
        }
        private async Task LoadData()
        {
            var temp_username = CurrentUserService.CurrentUser.UserName;
            StateHasChanged();
        }
    }
}
