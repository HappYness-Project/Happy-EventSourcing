using BlazorUI.Services.ItemEdit;
using HP.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components
{
    public partial class ItemElement<TItem> : ComponentBase where TItem : BaseItem
    {
        [Parameter] public RenderFragment MainFragment { get; set; }
        [Parameter] public RenderFragment DetailFragment { get; set; }
        [Parameter] public TItem Item { get; set; }
        [CascadingParameter] public string ColorPrefix { get; set; }
        [CascadingParameter] public int TotalNumber { get; set; }
        [Inject] private ItemEditService ItemEditService { get; set; }
        private string DetailAreaId { get; set; }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            DetailAreaId = "detailArea" + Item.Position;
        }
        private void OpenItemInEditMode()
        {
            ItemEditService.EditItem = Item;
        }
    }
}
