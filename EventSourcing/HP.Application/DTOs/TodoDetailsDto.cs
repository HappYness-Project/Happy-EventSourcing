using HP.Domain;
namespace HP.Application.DTOs
{
    public record TodoDetailsDto
    {
        public string UserName { get; set; }
        public string TodoId { get; set; }
        public string TodoTitle { get; set; }
        public string TodoType { get; set; }
        public TodoStatus TodoStatus { get; set; }
        
    }
}
