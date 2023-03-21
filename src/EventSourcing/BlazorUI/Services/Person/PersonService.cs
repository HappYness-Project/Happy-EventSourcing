using BlazorUI.Data;
using HP.Application.DTOs;
using HP.Core.Commands;
using HP.Shared.Common;
using HP.Shared.Contacts;
using HP.Shared.Requests.Persons;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;

namespace BlazorUI.Services.Person
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _httpClient;
        private AppSettings _appSettings { get; }
        private readonly ICurrentUserService _currentUserService;
        public PersonService(HttpClient httpClient, IOptions<AppSettings> appSettings, ICurrentUserService currentUserService)
        {
            _appSettings = appSettings.Value;
            httpClient.BaseAddress = new Uri(_appSettings.HpApiBaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient = httpClient;
            _currentUserService = currentUserService;
        }
        public async Task<Result<CommandResult>> CreateAsync(CreatePersonDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("persons", request);
            if (response.IsSuccessStatusCode)
                return new Result<CommandResult> { IsSuccess = false, Msg = $"Failed to Create Person. {request.PersonName}" };

            return new Result<CommandResult> { IsSuccess = true, Msg = $"Success to create a person." };
        }
        public async Task<Result<CommandResult>> UpdateAsync(string personId, UpdatePersonDto request)
        {
            var response = await _httpClient.PutAsJsonAsync($"persons/{personId}", request);
            if (!response.IsSuccessStatusCode)
                return new Result<CommandResult> { IsSuccess = false, Msg = $"Failed to update Person." };

            return new Result<CommandResult> { IsSuccess = true, Msg = $"Success to update a person." };
        }
        public async Task<Result<PersonDetailsDto>> GetPersonByPersonId(string id)
        {
            PersonDetailsDto? response = await _httpClient.GetFromJsonAsync<PersonDetailsDto>($"persons/{id}");
            return new Result<PersonDetailsDto> { IsSuccess = true, Data = response, Msg = $"Success to get the person data. personId: {id}" };
        }
        public async Task<IEnumerable<PersonDetailsDto>> GetPeopleList()
        {
            var persons = await _httpClient.GetFromJsonAsync<IEnumerable<PersonDetailsDto>>($"persons");
            return persons;
        }
    }
}
