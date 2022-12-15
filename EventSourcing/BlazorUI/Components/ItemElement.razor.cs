using BlazorUI.Services.ItemEdit;
using HP.Shared;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace BlazorUI.Components
{
    public partial class ItemElement<TItem> : ComponentBase where TItem : BaseItem
    {
        [Parameter] public RenderFragment MainFragment { get; set; }
        [Parameter] public RenderFragment DetailFragment { get; set; }
        [Parameter] public TItem Item { get; set; }
        [CascadingParameter] public string ColorPrefix { get; set; }
        [CascadingParameter] public int TotalNumber { get; set; }
        //[Inject] private ItemEditService ItemEditService { get; set; }
        private NavigationManager NavigationManager { get; set; }
        private string DetailAreaId { get; set; }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            DetailAreaId = "detailArea" + Item.Position;
        }
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                Item.PropertyChanged += HandleItemPropertyChanged;
            }
        }
        private void HandleItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            StateHasChanged(); 
        }
        private void OpenItemInEditMode()
        {
            //ItemEditService.EditItem = Item;
            Uri.TryCreate("/items/" + Item.ItemTypeEnum + "/" + Item.Id, UriKind.Relative, out var uri);
            NavigationManager.NavigateTo(uri.ToString());   
        }
    }
}
