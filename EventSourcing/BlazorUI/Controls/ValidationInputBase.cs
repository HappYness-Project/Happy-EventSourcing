using Microsoft.AspNetCore.Components;

namespace BlazorUI.Controls
{
    public class ValidationInputBase : ComponentBase
    {
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public string Value { get; set; }
        [Parameter]
        public string Error { get; set; }
        protected async void HandleInputChanged(ChangeEventArgs eventArgs)
        {
            await ValueChanged.InvokeAsync(eventArgs.Value.ToString());
        }

    }
}
