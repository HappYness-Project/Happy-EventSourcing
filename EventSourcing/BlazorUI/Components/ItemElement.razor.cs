using HP.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components
{
    public partial class ItemElement<TItem> : ComponentBase where TItem : BaseItem
    {
        [Parameter]
        public TItem Item { get; set; }
    }
}
