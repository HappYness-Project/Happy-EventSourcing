using HP.Application.DTOs;
using HP.Application.Queries.Todos;
using HP.Shared;
using HP.Shared.Contacts;
using MediatR;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace BlazorUI.Components
{
    public partial class TodosList : ComponentBase
    {
        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }

        [Inject]
        public IMediator Mediator { get; set; }
        protected ObservableCollection<TodoDetailsDto> Todos { get; set; } = new ObservableCollection<TodoDetailsDto>();
        protected override async void OnInitialized()
        {
            base.OnInitializedAsync();
            //Todos = CurrentUserService.CurrentUser.TodoItems;
        }

    }
}
