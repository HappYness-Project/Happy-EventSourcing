using BlazorUI.Services.ItemEdit;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Pages
{
    public partial class ItemsOverview : ComponentBase
    {
        [Inject] private ItemEditService ItemEditService { get; set; }
        private bool ShowEdit { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            ItemEditService.EditItemChanged += HandlerEditItemChanged;
        }

        private void HandlerEditItemChanged(object? sender, ItemEditEventArgs e)
        {
            ShowEdit = e.Item != null;
            StateHasChanged();
        }
    }
}
