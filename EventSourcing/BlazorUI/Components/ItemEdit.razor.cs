using BlazorUI.Services.ItemEdit;
using HP.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components
{
    public partial class ItemEdit
    {
        [Inject] private ItemEditService ItemEditService { get; set; }
        private BaseItem Item { get; set; } = new BaseItem();
        private int TotalNumber { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            ItemEditService.EditItemChanged += HandleEditItemChanged;
            Item = ItemEditService.EditItem;
        }

        private void HandleEditItemChanged(object? sender, ItemEditEventArgs e)
        {
            Item = e.Item;
            StateHasChanged();
        }
    }
}
