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
        public DateTime TargetStartDate { get; set; } = DateTime.Now;
        public DateTime TargetCompletedDate { get; set; }
    }
}