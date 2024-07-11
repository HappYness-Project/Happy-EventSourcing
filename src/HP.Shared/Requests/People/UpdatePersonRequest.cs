namespace HP.Shared.Requests.People
{
    public record UpdatePersonRequest
    {
        public string PersonType { get; set; }
        public int GroupId { get; set; }
        public string GoalType { get; set; }
    }
}
