namespace HP.Application.Commands
{
    public record UpdateTodoCommand(string TodoId, string Title, string Description, string[] Tags) : CommandBase<bool>;
}
