using BlazorUI.Data;
using BlazorUI.Pages;
using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Core.Commands;
using HP.Domain;
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
        public async Task<Result<IEnumerable<TodoDetailsDto>>> GetTodos()
        {
            // Todo need to get Hyunbin7303 from the IAuthenService!!!!
            string tmp_username = "hyunbin7303";
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
            string tmp_username = "hyunbin7303";
            var response = await _httpClient.PostAsJsonAsync($"Todos/Create/{tmp_username}", request);
            if (response.IsSuccessStatusCode)
            {
                return new Result<CommandResult> { IsSuccess = true, Data = new CommandResult(true, response.Content.ToString()) };
            }
            return new Result<CommandResult> { IsSuccess = false, Msg = response.Content.ToString() };
        }


    }
}
