namespace HP.Shared.Requests.Todos
{
    public record UpdateTodoDto
    {
        public string TodoId { get; set; }
        public string TodoTitle { get; set; }
        public string TodoType { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public DateTime? TargetStartDate { get; set; }
        public DateTime? TargetEndDate { get; set; }
    }
}
