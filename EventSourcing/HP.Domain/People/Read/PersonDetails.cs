using HP.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HP.Domain.People.Read
{
    [Table("PersonDetails")]
    public class PersonDetails : BaseEntity, IReadModel
    {
        public PersonDetails(Guid id)
        {
            this.Id = id;
            ProjectId = 0;
            GroupId = 0;
            IsActive = true;
            this.PersonType = "Normal";
            GoalType = "TBD";
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
