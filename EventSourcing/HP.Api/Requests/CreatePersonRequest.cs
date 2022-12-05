using System.ComponentModel.DataAnnotations;
namespace HP.Api.Requests
{
    public class CreatePersonRequest
    {
        [Required] public string PersonId { get; set; }
        [Required] public string UserType { get; set; }
    }
}