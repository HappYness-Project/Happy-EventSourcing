namespace HP.Shared.Requests.Todos
{
    public record TodoStatusChangeRequest(string status, string? reason = null);

    public class GetTodoItemByStatusDto
    {
        public string Status { get; set; }
    }
}
