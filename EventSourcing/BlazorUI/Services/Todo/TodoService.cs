using BlazorUI.Data;
using HP.Application.DTOs;
using HP.Domain;
using HP.Shared;
using HP.Shared.Contacts;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

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
            httpClient.BaseAddress = new Uri(_appSettings.HpApiBaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient = httpClient;
        }
        public async Task<ServiceResult<TodoDetailsDto>> GetTodoDetails(string TodoId)
        {
            var todo = await _httpClient.GetFromJsonAsync<TodoDetailsDto>($"Todos/{TodoId}");
            ServiceResult<TodoDetailsDto> result = new();
            result.IsSuccess = true;
            result.Result = todo;
            return result;
        }

        public async Task<ServiceResult<int>> GetTodoItemsCount(bool OnlyActive = true)
        {
            throw new NotImplementedException();
        }
    }
}
