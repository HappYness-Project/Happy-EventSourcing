using HP.Application.DTOs;
using HP.Shared;
using HP.Shared.Contacts;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace BlazorUI.Components
{
    public partial class ItemsList : ComponentBase
    {
        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }
        //protected ObservableCollection<TodoBasicInfoDto> TodoItems { get; set; } = new ObservableCollection<TodoBasicInfoDto>();
        protected ObservableCollection<BaseItem> UserItems { get; set; } = new ObservableCollection<BaseItem>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            UserItems = CurrentUserService.CurrentUser.TodoItems;
        }
    }
}
