using BlazorUI.Data;
using HP.Core.Commands;
using HP.Shared.Common;
using HP.Shared.Contacts;
using HP.Shared.Requests.People;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

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
        public async Task<Result<CommandResult>> CreateAsync(CreatePersonRequest request)
        {

            var getuser = _currentUserService.CurrentUser.UserName;
            var response = await _httpClient.PostAsJsonAsync("create", request);
            if(response.IsSuccessStatusCode)
                return new Result<CommandResult> { IsSuccess = false,  Msg = $"Failed to Create Person. {request.PersonId}" };

            return new Result<CommandResult> { IsSuccess = true, Msg = $"Success to create a person." };
        }
        public async Task<Result<CommandResult>> UpdateAsync(UpdatePersonRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("people/Update", request);
            if (!response.IsSuccessStatusCode)
                return new Result<CommandResult> { IsSuccess = false,  Msg = $"Failed to update Person. {request.PersonId}" };

            return new Result<CommandResult> { IsSuccess = true, Msg = $"Success to update a person. {request.PersonId}" };
        }
    }
}
