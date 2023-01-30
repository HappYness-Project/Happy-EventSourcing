using HP.Core.Models;
namespace HP.Domain.People.Read
{
    public class PersonDetails : BaseEntity, IReadModel
    {
        public PersonDetails(Guid id)
        {
            this.Id = id;
        }
        public string PersonName { get; set; }
        public string PersonType { get; set; }
        public string PersonRole { get; set; }
        public bool IsActive { get; set; }
        public int ProjectId { get; set; }
        public int GroupId { get; set; }
        public string GoalType { get; set; }

    }
}
