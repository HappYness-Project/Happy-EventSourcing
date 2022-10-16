using System.ComponentModel.DataAnnotations;

namespace HP.Shared
{
    // GOing to be used for the Identity service in the future.
    public class User
    {
        [Required]
        [StringLength(15, ErrorMessage ="User name is too long.")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="The password is required.")]
        public string Password { get; set; }

    }
}