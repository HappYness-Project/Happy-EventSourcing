using BlazorUI.Data;
using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Core.Commands;
using HP.Shared;
using HP.Shared.Contacts;
using HP.Shared.Requests.Todos;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

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
        public async Task<Result<TodoDetailsDto>> GetTodoDetails(string TodoId)
        {
            var todo = await _httpClient.GetFromJsonAsync<TodoDetailsDto>($"Todos/{TodoId}");
            Result<TodoDetailsDto> result = new();
            result.IsSuccess = true;
            result.Data = todo;
            return result;
        }
        public async Task<Result<int>> GetTodoItemsCount(bool OnlyActive = true)
        {
            throw new NotImplementedException();
        }

        public Task<Result<CommandResult>> CreateAsync(CreateTodoItemModel createTodoItemModel)
        {
            throw new NotImplementedException();
        }
    }
}
