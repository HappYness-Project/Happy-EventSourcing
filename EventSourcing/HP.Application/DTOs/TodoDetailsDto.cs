using HP.Domain;

namespace HP.Application.DTOs
{
    public record TodoDetailsDto
    {
        public string UserId { get; set; }
        public string TodoId { get; set; }
        public string TodoTitle { get; set; }
        public string Description { get; set; }
        public string TodoType { get; set; }
        public bool IsActive { get; set; }
        public bool IsDone { get; set; }
        public string TodoStatus { get; set; }
        public ICollection<TodoItem> SubTodos { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CompletedDateTime { get; set; }
        public DateTime? TargetStartDate { get; set; }
        public DateTime? TargetEndDate { get; set; }
    }
}
