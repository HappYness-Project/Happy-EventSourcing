using HP.Domain;
using System.ComponentModel.DataAnnotations;
namespace HP.Api.Requests
{
    public record UpdateStatusTodoItemRequest([Required]string TodoId, [Required]string TodoItemId, [Required]string NewStatus);
}