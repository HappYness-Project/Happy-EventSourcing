using BlazorUI.Data;
using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Core.Commands;
using HP.Shared.Common;
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
        private readonly CurrentUserService _currentUserService;
        public event Action OnChange;
        public TodoService(HttpClient httpClient, IOptions<AppSettings> appSettings, CurrentUserService currentUserService)
        {
            _appSettings = appSettings.Value;
            httpClient.BaseAddress = new Uri(_appSettings.HpApiBaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient = httpClient;
            _currentUserService = currentUserService;
        }
        public async Task<Result<IEnumerable<TodoDetailsDto>>> GetTodos()
        {
            string tmp_username = _currentUserService.CurrentUser.UserName;
            IEnumerable<TodoDetailsDto> todos = await _httpClient.GetFromJsonAsync<IEnumerable<TodoDetailsDto>>($"Todos/users/{tmp_username}");
            return new()
            {
                IsSuccess = true,
                Data = todos,
                Msg = "Return Todos"
            };
        }
        public async Task<Result<TodoDetailsDto>> GetTodoDetails(string TodoId)
        {
            var todo = await _httpClient.GetFromJsonAsync<TodoDetailsDto>($"Todos/{TodoId}");
            if (todo == null)
            {
                return new() { IsSuccess = true, Data = null, Msg = "Request was successful " };
            }
            return new() { IsSuccess = true, Data = todo };
        }
        public async Task<Result<int>> GetTodoItemsCount(bool OnlyActive = true)
        {
            throw new NotImplementedException();
        }
        public async Task<Result<CommandResult>> CreateAsync(CreateTodoRequest request)
        {
            string tmp_username = _currentUserService.CurrentUser.UserName;
            var response = await _httpClient.PostAsJsonAsync($"Todos/Create/{tmp_username}", request);
            if (response.IsSuccessStatusCode)
            {
                return new Result<CommandResult> { IsSuccess = true, Data = new CommandResult(true, response.Content.ToString()) };
            }
            return new Result<CommandResult> { IsSuccess = false, Msg = response.Content.ToString() };
        }

        public Task<Result<string>> UpdateAsync(UpdateTodoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
