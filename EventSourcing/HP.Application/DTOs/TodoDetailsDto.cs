namespace HP.Application.DTOs
{
    public record TodoDetailsDto
    {
        public string UserId { get; set; }
        public string TodoId { get; set; }
        public string TodoTitle { get; set; }
        public string TodoType { get; set; }
    }
}
