using HP.Application.Commands.Todos;
using HP.Application.DTOs;
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
        private string DeleteTodoId { get; set; } = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }
        protected void OnClickViewDetails(string todoId)
        {
            NavigationManager.NavigateTo($"todos/details/{todoId}");
        }
        protected async void OpenDeleteDialog(string todoId)
        {
            DeleteDialogOpen = true;
            DeleteTodoId = todoId;
            await LoadData();
        }
        protected async Task OnDeleteDialogClose(bool accepted)
        {
            if (accepted)
            {
                await _mediator.Send(new DeleteTodoCommand(Guid.Parse(DeleteTodoId)));
                DeleteTodoId = null;
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
