using HP.Domain.Common;
namespace HP.Domain
{
    public class TodoStatus : Enumeration
    {
        public TodoStatus(int id, string name) : base(id, name){ }
        public static TodoStatus Pending => new(0, "Pending");
        public static TodoStatus Accepted => new(1, "Accepted");
        public static TodoStatus Started => new(2, "Started");
        public static TodoStatus Completed => new(3, "Completed");
        public static TodoStatus Stopped => new(4, "Stopped");
        public static TodoStatus WontDo => new(5, "WontDo");

    }

}
