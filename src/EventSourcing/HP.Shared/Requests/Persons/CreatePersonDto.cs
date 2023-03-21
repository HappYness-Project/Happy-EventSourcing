using System.ComponentModel.DataAnnotations;
namespace HP.Shared.Requests.Persons
{
    public record CreatePersonDto
    {
        [Required] public string PersonName { get; set; }
        [Required] public string PersonType { get; set; }
        public int GroupId { get; set; }
    }

}
