namespace HP.Shared.Requests.People
{
    public record UpdatePersonRequest
    {
        public string PersonId { get; set; }
        public string PersonType { get; set; }
        public int GroupId { get; set; }

    }

}
