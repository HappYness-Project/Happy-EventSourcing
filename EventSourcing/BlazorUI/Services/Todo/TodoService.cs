using BlazorUI.Data;
using HP.Application.DTOs;
using HP.Shared;
using HP.Shared.Contacts;
using Microsoft.Extensions.Options;

namespace BlazorUI.Services.Todo
{
    public class TodoService : ITodoService
    {
        private readonly HttpClient _httpClient;
        private AppSettings _appSettings { get; } 
        //private readonly IAuthenService _auithService;
        public event Action OnChange;
        public TodoService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            this._httpClient.BaseAddress = new Uri(_appSettings.HpApiBaseAddress);
            this._httpClient = httpClient;
        }

        public async Task<ServiceResult<TodoDetailsDto>> GetTodoDetails(string TodoId)
        {
            var result = await _httpClient.GetFromJsonAsync<TodoDetailsDto>($"api/todos/{TodoId}");
            //return result;
            return null;
        }

        public async Task<ServiceResult<int>> GetTodoItemsCount(bool OnlyActive = true)
        {
            throw new NotImplementedException();
        }
    }
}
