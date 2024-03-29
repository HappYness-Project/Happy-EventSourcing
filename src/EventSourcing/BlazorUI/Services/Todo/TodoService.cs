﻿using BlazorUI.Data;
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
        private readonly ICurrentUserService _currentUserService;
        private readonly HttpClient _httpClient;
        public event Action TodoChanged;
        private AppSettings _appSettings { get; }
        public TodoDetailsDto Todo { get; set; } = new();
        public IEnumerable<TodoItemDto> CompletedTodoItems { get; set; } = new List<TodoItemDto>(); 
        public TodoService(HttpClient httpClient, IOptions<AppSettings> appSettings, ICurrentUserService currentUserService)
        {
            _appSettings = appSettings.Value;
            httpClient.BaseAddress = new Uri(_appSettings.HpApiBaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient = httpClient;
            _currentUserService = currentUserService;
        }
        public async Task<Result<TodoDetailsDto>> GetTodoById(string? todoId = null)
        {
            todoId = todoId == null ? Todo.TodoId : todoId;
            var todo = await _httpClient.GetFromJsonAsync<TodoDetailsDto>($"Todos/{todoId}");
            if (todo == null)
                return new Result<TodoDetailsDto> { IsSuccess = false, Msg = "Not able to get the data" };
            
            TodoChanged?.Invoke();
            return new Result<TodoDetailsDto> { IsSuccess = true, Data = todo, Msg = "GetTodoById success." };
        }
        public async Task<Result<int>> GetTodoItemsCount(bool OnlyActive = true)
        {
            throw new NotImplementedException();
        }
        public async Task<CommandResult> CreateAsync(CreateTodoDto request)
        {
            string userName = _currentUserService.CurrentUser.UserName;
            using HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"todos/{userName}", request);
            if (!response.IsSuccessStatusCode)
                return new CommandResult { IsSuccess = false, Message = response.Content.ToString() };
            return new CommandResult { IsSuccess = true, Message = response.Content.ToString() };
        }
        public async Task<CommandResult> UpdateAsync(UpdateTodoDto request)
        {
            var response = await _httpClient.PutAsJsonAsync($"Todos/{request.Id}", request);
            if (!response.IsSuccessStatusCode)
                return new CommandResult { IsSuccess = false, Message = response.Content.ToString() };

            return new CommandResult { IsSuccess = true, Message = $"TodoId:{request.Id} has been updated." };
        }
        public async Task<Result<IEnumerable<TodoDetailsDto>>> GetTodosByPersonId(string? PersonName = "")
        {
            string? tmp_username = PersonName != "" ? PersonName : _currentUserService.CurrentUser.UserName;
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<TodoDetailsDto>>($"Todos/Users/{tmp_username}");
            if (response == null)
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
        public async Task<Result<CommandResult>> UpdateTodoStatus(string todoId, string status)
        {
            var result = await _httpClient.PatchAsync($"todos/{todoId}/{status}", null);
            if (!result.IsSuccessStatusCode)
                return new Result<CommandResult> { IsSuccess = false, Data = null, Msg = $"Updating status for TodoId: {todoId} failed." };

            TodoChanged?.Invoke();
            return new Result<CommandResult> { IsSuccess = true, Data = null, Msg = $"Updating statusfor TodoId: {todoId} Success." };
        }
        public async Task<CommandResult> CreateTodoItemAsync(CreateTodoItemDto createTodoItem)
        {
            var response = await _httpClient.PostAsJsonAsync($"Todos/{Todo.TodoId}/todoItems", createTodoItem);
            if (!response.IsSuccessStatusCode)
                return new CommandResult { IsSuccess = false, Message = response.Content.ToString() };

            TodoChanged?.Invoke();
            return new CommandResult { IsSuccess = true, Message = response.Content.ToString() };
        }
        public async Task<CommandResult> UpdateTodoItemAsync(UpdateTodoItemDto todoItem)
        {

            var response = await _httpClient.PutAsJsonAsync($"Todos/{Todo.TodoId}/todoItems/{todoItem.TodoItemId}", todoItem);
            if (!response.IsSuccessStatusCode)
                return new CommandResult { IsSuccess = false, Message = response.Content.ToString() };

            return new CommandResult { IsSuccess = true, EntityId = todoItem.TodoItemId, Message = response.Content.ToString() };
        }
        public async Task<CommandResult> DeleteTodoItemAsync(string todoItemId)
        {
            var response = await _httpClient.DeleteAsync($"Todos/{Todo.TodoId}/todoItems/{todoItemId}");
            if (!response.IsSuccessStatusCode)
                return new CommandResult { IsSuccess = false, Message = response.Content.ToString() };
            
            return new CommandResult { IsSuccess = true, EntityId = todoItemId, Message = $"TodoItemId:{todoItemId} in Todo:{Todo.TodoId} has been deleted successfully." };
        }
        public async Task<IEnumerable<TodoItemDto>> GetTodoItemsById(string todoId)
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<TodoItemDto>>($"todos/{Todo.TodoId}/todoItems");
            TodoChanged?.Invoke();
            return response;
        }
        public async Task<IEnumerable<TodoItemDto>> GetTodoItemsByStatus(string status)
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<TodoItemDto>>($"Todos/{Todo.TodoId}/TodoItems/status/{status}");
            TodoChanged?.Invoke();
            return response;
        }
        public async Task<CommandResult> UpdateTodoItemStatus(string todoId, string todoItemId, string status)
        {
            var statusDto = new UpdateTodoStatusDto { Status = status };
            var response = await _httpClient.PutAsJsonAsync($"Todos/{todoId}/todoItems/{todoItemId}/status", statusDto);
            if (!response.IsSuccessStatusCode)
                return new CommandResult { IsSuccess = false, Message = response.Content.ToString() };

            TodoChanged?.Invoke();
            return new CommandResult { IsSuccess = true, EntityId = todoItemId, Message = $"The status of TodoItemId:{todoItemId} in Todo:{todoId} has been updated successfully." };
        }
        public async Task<CommandResult> ToggleActive(string todoId, bool activate)
        {
            HttpResponseMessage msg = null;
            if(activate)
                msg = await _httpClient.PatchAsync($"Todos/{todoId}/Activation", null);
            else
                msg = await _httpClient.PatchAsync($"Todos/{todoId}/Deactivation", null);

            if (!msg.IsSuccessStatusCode)
                return new CommandResult { IsSuccess = false, EntityId = todoId, Message = msg.Content.ToString() };
            return new CommandResult { IsSuccess = true, EntityId = todoId, Message = msg.Content.ToString() };

        }
        public async Task<CommandResult> ToggleTodoItemActive(string todoId, string todoItemId, bool activate)
        {
            HttpResponseMessage msg = null;
            if (activate)
                msg = await _httpClient.PatchAsync($"Todos/{todoId}/TodoItems/{todoItemId}/Activation", null);
            else
                msg = await _httpClient.PatchAsync($"Todos/{todoId}/TodoItems/{todoItemId}/Deactivation", null);

            if (!msg.IsSuccessStatusCode)
                return new CommandResult { IsSuccess = false, EntityId = todoItemId, Message = msg.Content.ToString() };
            return new CommandResult { IsSuccess = true, EntityId = todoItemId, Message = msg.Content.ToString() };
        }
        public async Task SetValue(TodoDetailsDto newTodo)
        {
            Todo = newTodo;
            TodoChanged?.Invoke();
        }
    }
}
