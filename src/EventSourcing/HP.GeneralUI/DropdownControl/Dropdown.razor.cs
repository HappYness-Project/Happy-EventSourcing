﻿using Microsoft.AspNetCore.Components;


namespace HP.GeneralUI.DropdownControl
{
    // Dropdown component
    // - set a default value and get change notification (two way binding)
    // - Provide a list with selectable items
    // - Set title of selected.

    public partial class Dropdown<TValue> : ComponentBase
    {
        [Parameter]
        public IList<DropdownItem<TValue>> SelectableItems { get; set; }

        [Parameter]
        public DropdownItem<TValue> SelectedItem { get; set; }

        [Parameter]
        public EventCallback<DropdownItem<TValue>> SelectedItemChanged { get; set; }
        public async void OnItemClicked(DropdownItem<TValue> item)
        {
            SelectedItem = item;
            StateHasChanged();
            await SelectedItemChanged.InvokeAsync(item);
        }
    }
}
