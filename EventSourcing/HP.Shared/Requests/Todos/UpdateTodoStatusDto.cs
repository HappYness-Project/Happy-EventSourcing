namespace HP.Shared.Requests.Todos
{
    public class UpdateTodoStatusDto
    {
        public string Status { get; set; }
        public string? Reason { get; set; }
    }
}
