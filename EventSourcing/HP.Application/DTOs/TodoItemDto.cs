namespace HP.Application.DTOs
{
    public record TodoItemDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string TodoType { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDone { get; set; }
        public string TodoStatus { get; set; }
        public DateTime TargetStartDate { get; set; } = DateTime.Now;
        public DateTime TargetCompletedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CompletedDate { get; set; }

    }
}
