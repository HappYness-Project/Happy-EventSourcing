using System.ComponentModel.DataAnnotations;
namespace HP.Shared.Requests.Todos
{
    public record CreateTodoItemDto
    {
        [Required]
        [StringLength(50)]
        public string TodoTitle { get; set; }
        public string Description { get; set; } = string.Empty; 
        public string TodoType { get; set; } = string.Empty;
        public string TodoStatus { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public DateTime TargetDate { get; set; }
    }
}