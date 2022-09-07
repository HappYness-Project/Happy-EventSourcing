using HP.Application.Commands;
using HP.Application.DTOs;

namespace HP.Application.Handlers
{
    public record CreateTodoCommand(string todoTitle, string todoType, string Description = null,  string[] Tag = null) : CommandBase<TodoDetailsDto>
    {
        public string UserId { get; set; }
        public string TodoTitle { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int MyProperty { get; set; }

    }
}
