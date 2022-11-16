using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Application.Queries.Todos;
using HP.Shared;
using HP.Shared.Contacts;
using MediatR;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace BlazorUI.Components.Todo
{
    public partial class DisplayTodoList : ComponentBase
    {
        [Inject] public IMediator _mediator { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ICurrentUserService CurrentUserService { get; set; }
        public TodoDetailsDto SelectedTodo { get; set; }
        public Type DynamicComponentType { get; set; }
        public IEnumerable<TodoDetailsDto> TodoDetailsDtos { get; set; } = new List<TodoDetailsDto>();
        public Dictionary<string, object> DynamicComponentParams { get; set; }
        public bool DeleteTodoDialogOpen { get; set; } = false;
        public string _deleteTodoId { get; set; }

        //public EventCallback<TodoEventArgs> DataButtonClickHandler { get; set; }
        protected override async Task OnInitializedAsync()
        {
            base.OnInitializedAsync();
            var username = CurrentUserService.CurrentUser.UserName;
            TodoDetailsDtos = await _mediator.Send(new GetTodosByUserId(username));
            //Todos = CurrentUserService.CurrentUser.TodoItems;
        }
        public void OnClickViewDetails(string todoId)
        {
            NavigationManager.NavigateTo($"todos/details/{todoId}");
        }
        protected async Task OnDeleteDialogClose(bool accepted)
        {
            if (accepted)
            {
                await _mediator.Send(new DeleteTodoCommand(_deleteTodoId));
                _deleteTodoId = string.Empty;
            }
            DeleteTodoDialogOpen = false;
            StateHasChanged();
        }
        protected async void OpenDeleteDialog(string todoId)
        {
            DeleteTodoDialogOpen = true;
            _deleteTodoId = todoId;
            await LoadData();
        }
        private async Task LoadData()
        {
            var temp_username = CurrentUserService.CurrentUser.UserName;
            TodoDetailsDtos = await _mediator.Send(new GetTodosByUserId(temp_username));
            StateHasChanged();
        }
    }
}
