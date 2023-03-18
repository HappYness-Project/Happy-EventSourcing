using HP.Application.DTOs;
using HP.Shared.Contacts;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components.Person
{
    public partial class DisplayPersonList : ComponentBase
    {
        [Inject] private IPersonService _personService { get; set; }
        List<PersonDetailsDto> people = new();
        protected override async Task OnInitializedAsync()
        {
            var people = await _personService.GetPeopleList();
            if (people != null)
                this.people = people.ToList();
        }
    }
}
