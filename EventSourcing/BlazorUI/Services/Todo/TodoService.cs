using HP.Shared.Contacts;

namespace BlazorUI.Services.Todo
{
    public class TodoService : ITodoService
    {
        private readonly HttpClient httpClient;
        public TodoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
    }
}
