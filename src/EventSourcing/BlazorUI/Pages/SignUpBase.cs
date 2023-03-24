using HP.GeneralUI.DropdownControl;
using HP.Shared;
using HP.Shared.Contacts;
using HP.Shared.Enums;
using HP.Shared.Requests.Persons;
using HP.Shared.Requests.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace BlazorUI.Pages
{
    public class SignUpBase : SignBase
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IUserManager _userManager { get; set; }
        [Inject] private IPersonService _personService { get; set; }
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
            SelectedGenderTypeDropDownItem = male;
        }


        private void TryGetUserNameFromUri()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            StringValues sv;
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("userName", out sv))
            {
                User.UserName = sv;
            }
        }
        protected async Task OnValidSubmit()
        {
            CreateUserDto newUser = new()
            {
                UserName = User.UserName,
                Password = User.Password,
                Email = User.Email,
                FirstName = User.FirstName,
                LastName = User.LastName,
            };
            var response = await _userManager.RequestUserCreateAsync(newUser);
            if(response.IsSuccess)
            {
                CreatePersonDto request = new CreatePersonDto
                {
                    PersonName = User.UserName,
                    PersonType = "TBD"
                };
                await _personService.CreateAsync(request);
                NavigationManager.NavigateTo("signin");
            }
        }
    }
}
