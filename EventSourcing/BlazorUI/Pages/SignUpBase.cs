using HP.GeneralUI.DropdownControl;
using HP.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Pages
{
    public class SignUpBase : SignBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        protected IList<DropdownItem<GenderTypeEnum>> GenderTypeDropDownItems { get; } = new List<DropdownItem<GenderTypeEnum>>();
        protected DropdownItem<GenderTypeEnum> SelectedGenderTypeDropDownItem { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            var male = new DropdownItem<GenderTypeEnum>
            {
                ItemObject = GenderTypeEnum.Male,
                DisplayText = "Male"
            };
            var female = new DropdownItem<GenderTypeEnum>
            {
                ItemObject = GenderTypeEnum.Female,
                DisplayText = "Female"
            };
            var neutral = new DropdownItem<GenderTypeEnum>
            {
                ItemObject = GenderTypeEnum.Neutral,
                DisplayText = "Others"
            };

            GenderTypeDropDownItems.Add(male);
            GenderTypeDropDownItems.Add(female);
            GenderTypeDropDownItems.Add(neutral);
            SelectedGenderTypeDropDownItem = neutral;

        }
        protected void OnValidSubmit()
        {
            NavigationManager.NavigateTo("signin");
        }
    }
}
