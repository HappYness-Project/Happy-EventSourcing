using HP.Domain.Common;
namespace HP.Domain
{
    public class TodoStatus : Enumeration
    {
        public TodoStatus(int id, string name) : base(id, name){ }
        public static TodoStatus Pending => new(0, nameof(Pending).ToLowerInvariant());
        public static TodoStatus Accepted => new(1, nameof(Accepted).ToLowerInvariant());
        public static TodoStatus Started => new(2, nameof(Started).ToLowerInvariant());
        public static TodoStatus Completed => new(3, nameof(Completed).ToLowerInvariant());
        public static TodoStatus Stopped => new(4, nameof(Stopped).ToLowerInvariant());
        public static TodoStatus WontDo => new(5, nameof(WontDo).ToLowerInvariant());
    }
}
