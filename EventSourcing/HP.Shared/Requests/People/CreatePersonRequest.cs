using HP.Domain;
using System.ComponentModel.DataAnnotations;
namespace HP.Shared.Requests.People
{
    public record CreatePersonRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Address Address { get; set; }
        [Required]
        public string Email { get; set; }
    }

}
