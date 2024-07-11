using HP.Shared;

namespace BlazorUI.Services.ItemEdit
{
    public class ItemEditEventArgs : EventArgs
    {
        public BaseItem Item { get; set; }
    }
}
