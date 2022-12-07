using BlazorUI.Data;
using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Core.Commands;
using HP.Shared.Common;
using HP.Shared.Contacts;
using HP.Shared.Requests.Todos;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BlazorUI.Services.Todo
{
    public class TodoService : ITodoService
    {
        private readonly HttpClient _httpClient;
        private AppSettings _appSettings { get; }
        private readonly ICurrentUserService _currentUserService;
        public event Action OnChange;
        public TodoService(HttpClient httpClient, IOptions<AppSettings> appSettings, ICurrentUserService currentUserService)
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
        public async Task<Result<TodoDetailsDto>> GetTodoById(string todoId)
        {
            var todo = await _httpClient.GetFromJsonAsync<TodoDetailsDto>($"Todos/{todoId}");
            if(todo == null )
                return new Result<TodoDetailsDto> { IsSuccess = false, Msg = "Not able to get the data" };

            return new Result<TodoDetailsDto> { IsSuccess = true, Data = todo, Msg = "GetTodoById success." };
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
                return new Result<CommandResult> { IsSuccess = true, Data = new CommandResult(true, response.Content.ToString()) };
            return new Result<CommandResult> { IsSuccess = false, Msg = response.Content.ToString() };
        }
        public async Task<Result<CommandResult>> UpdateAsync(UpdateTodoRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"Todos/Update", request);
            if (!response.IsSuccessStatusCode)
                return new Result<CommandResult> { IsSuccess = false, Msg = response.Content.ToString() };
            return new Result<CommandResult> { IsSuccess = true, Data = new CommandResult(true, response.Content.ToString()), Msg = $"TodoId:{request.TodoId} has been updated." };
        }
        public async Task<Result<IEnumerable<TodoDetailsDto>>> GetTodosByPersonId(string? PersonName = "")
        {
            string? tmp_username = PersonName != "" ? PersonName : _currentUserService.CurrentUser.UserName;
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<TodoDetailsDto>>($"Todos/Users/{tmp_username}");
            if(response == null)
            {
                var check = new Result<IEnumerable<TodoDetailsDto>>();
                check.IsSuccess = false;
                check.Msg = $"Failed to retrieve data for PersonId: {PersonName}";
                return check;
            }
            var result = new Result<IEnumerable<TodoDetailsDto>>
            {
                IsSuccess = true,
                Msg = "Successful for retrieving data from the GetTodosByPersonId.",
                Data = response
            };
            return result;
        }
        public async Task<Result<CommandResult>> DeleteAsync(string todoId)
        {
            var result = await _httpClient.DeleteAsync($"todos/{todoId}");
            if (!result.IsSuccessStatusCode)
                return new Result<CommandResult> { IsSuccess = false, Data = null, Msg = $"Deleting TodoId: {todoId} failed." };
            return new Result<CommandResult> { IsSuccess = true, Data = null, Msg = $"Deleting TodoId: {todoId} Success." };
        }
    }
}
