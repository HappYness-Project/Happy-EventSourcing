namespace HP.Shared.Requests.Persons
{
    public record UpdatePersonDto
    {
        public string PersonType { get; set; }
        public int GroupId { get; set; }
        public string GoalType { get; set; }
    }
}
