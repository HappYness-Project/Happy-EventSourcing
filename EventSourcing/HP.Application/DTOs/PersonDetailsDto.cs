namespace HP.Application.DTOs
{
    public record PersonDetailsDto
    {
        public string Id { get; set; }
        public string UserId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonType { get; set; }
        public string AddressStr { get; set; }
        public string Email { get; set; }
        public string GoalType { get; set; }
        public int ProjectId { get; set; }
    }
}
