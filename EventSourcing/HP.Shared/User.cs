using System.ComponentModel.DataAnnotations;

namespace HP.Shared
{
    // GOing to be used for the Identity service in the future.
    public class User
    {
        [Required]
        public string UserName { get; set; }
        [Required(ErrorMessage ="The password is required.")]
        public string Password { get; set; }

    }
}