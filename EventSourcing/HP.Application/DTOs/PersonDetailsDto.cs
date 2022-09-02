namespace HP.Application.DTOs
{
    public record PersonDetailsDto
    {
        public PersonDetailsDto() { }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressStr { get; set; }
        public string Email { get; set; }
    }
}
