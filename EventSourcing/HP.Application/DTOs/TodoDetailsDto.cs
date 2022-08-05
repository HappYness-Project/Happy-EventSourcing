using HP.Domain.Todos;

namespace HP.Application.DTOs
{
    public class TodoDetailsDto
    {
        public string UserId { get; set; }
        public string TodoId { get; set; }
        public string TodoTitle { get; set; }
        public string TodoType { get; set; }
        public TodoStatus TodoStatus { get; set; }
    }
}
