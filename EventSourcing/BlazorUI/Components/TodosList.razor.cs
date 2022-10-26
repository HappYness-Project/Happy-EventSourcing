using HP.Application.DTOs;
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
        public IMediator Mediator { get; set; }
        protected ObservableCollection<TodoDetailsDto> Todos { get; set; } = new ObservableCollection<TodoDetailsDto>();
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
