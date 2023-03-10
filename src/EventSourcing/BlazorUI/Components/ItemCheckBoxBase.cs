using HP.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components
{
    public class ItemCheckBoxBase : ComponentBase
    {
        [Parameter]
        public BaseItem Item { get; set; }
        [CascadingParameter]
        public string ColorPrefix { get; set; }
    }
}
