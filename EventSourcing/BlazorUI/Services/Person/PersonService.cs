using BlazorUI.Data;
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
        private readonly CurrentUserService _currentUserService;
        public PersonService(HttpClient httpClient, IOptions<AppSettings> appSettings, CurrentUserService currentUserService)
        {
            _appSettings = appSettings.Value;
            httpClient.BaseAddress = new Uri(_appSettings.HpApiBaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient = httpClient;
            _currentUserService = currentUserService;
        }
        public async Task CreateAsync(CreatePersonRequest request)
        {
            // Assumption here : The user has been created already.
            var response = await _httpClient.PostAsJsonAsync("create", request);
            if(response.IsSuccessStatusCode)
            {
                // TODO Log msg in here.
            }
        }
        public async Task UpdatePersonAsync(UpdatePersonRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("Update", request);
        }
    }
}
