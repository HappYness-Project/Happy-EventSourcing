using System.ComponentModel.DataAnnotations;
namespace HP.Shared.Requests.Categories
{
    public record CreateCategoryRequest
    {
        [Required]
        public string PersonName { get; set; }
    }
}