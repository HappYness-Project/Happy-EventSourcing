using System.ComponentModel.DataAnnotations;
namespace HP.Shared.Requests.Categories
{
    public record CreateCategoryRequest
    {
        [Required] public string CategoryName { get; set; }
        [Required] public string CategoryType { get; set; }
        [Required] public string CategoryDesc { get; set; }
    }
}