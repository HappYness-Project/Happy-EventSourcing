using System.ComponentModel.DataAnnotations;
namespace HP.Shared.Requests.Todos
{
    public record CreateTodoItemRequest
    {
        [Required]
        public string TodoId { get; set; }
        [Required]
        [StringLength(50)]
        public string TodoTitle { get; set; }
        public string Description { get; set; }
        public string TodoType { get; set; }
        public string TodoStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public DateTime TargetDate { get; set; }
    }
}
//string TodoTitle, string TodoType, string Description, string[] Tags = null