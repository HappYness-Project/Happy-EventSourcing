using Microsoft.AspNetCore.Components;

namespace BlazorUI.Controls
{
    public class ValidationInputBase : ComponentBase
    {
        [Parameter]
        public EventCallback<string> ValueChangedCallBack { get; set; }

        [Parameter]
        public string Value { get; set; }
        protected async void HandleInputChanged(ChangeEventArgs eventArgs)
        {
            await ValueChangedCallBack.InvokeAsync(eventArgs.Value.ToString());
        }

    }
}
