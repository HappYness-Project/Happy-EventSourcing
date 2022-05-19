namespace HP.Api.DTO
{
    public record UpdateTodoDto(string TodoId, string TodoTotle, string TodoDescription, string[] Tags);
}
