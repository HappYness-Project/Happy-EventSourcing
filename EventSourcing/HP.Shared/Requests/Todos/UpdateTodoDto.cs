namespace HP.Shared.Requests.Todos
{
    public record UpdateTodoDto
    {
        public string TodoId { get; set; }
        public string TodoTitle { get; set; } = string.Empty;
        public string TodoType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? TargetStartDate { get; set; }
        public DateTime? TargetEndDate { get; set; } 
    }
}
