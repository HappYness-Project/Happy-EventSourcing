using System.ComponentModel.DataAnnotations;
namespace HP.Shared.Requests.Todos
{
    public record CreateTodoDto
    {
        [Required] public string Title { get; set; }
        public string TodoType { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; } = null;
        public DateTime? TargetEndDate { get; set; } = null;
        public string[] Tags { get; set; } = null;
    }
}
