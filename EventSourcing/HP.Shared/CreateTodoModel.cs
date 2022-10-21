using System.ComponentModel.DataAnnotations;

namespace HP.Shared
{
    public class CreateTodoModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string TodoType { get; set; }
    }


}