using HP.Application.DTOs;
using HP.Shared.Contacts;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace BlazorUI.Components
{
    public partial class ItemsList : ComponentBase
    {
        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }

        protected ObservableCollection<TodoBasicInfoDto> TodoItems { get; set; } = new ObservableCollection<TodoBasicInfoDto>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            TodoItems = CurrentUserService.CurrentUser.TodoItems;
        }
    }
}
