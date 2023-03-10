using System.ComponentModel.DataAnnotations;
namespace HP.Shared.Requests.Todos
{
    public class CreateTodoDto
    {
        [Required] public string Title { get; set; }
        public string TodoType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? TargetStartDate { get; set; } = null;
        public DateTime? TargetEndDate { get; set; } = null;
    }
}
