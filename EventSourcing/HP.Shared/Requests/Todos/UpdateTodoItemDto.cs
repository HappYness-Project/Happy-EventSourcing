using System.ComponentModel.DataAnnotations;
namespace HP.Shared.Requests.Todos
{
    public record UpdateTodoItemDto
    {
        [Required]
        [StringLength(50)]
        public string TodoItemId { get; set; }
        public string TodoTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TodoType { get; set; } = string.Empty;
        public DateTime TargetStartDate { get; set; } = DateTime.Now;
        public DateTime TargetCompletedDate { get; set; }
    }
}