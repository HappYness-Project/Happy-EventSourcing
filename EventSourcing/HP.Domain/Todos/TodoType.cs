using HP.Domain.Common;
namespace HP.Domain
{
    public class TodoType : Enumeration
    {
        public TodoType(int id, string name) : base(id, name){ }
        public static TodoType Others => new(0, "Others");
        public static TodoType Study => new(1, "Study");
        public static TodoType Research => new(2, "Research");
        public static TodoType Chores => new(3, "Chores");
        public static TodoType Work => new(4, "Work");

        public static IEnumerable<TodoType> List() =>
            new[] { Others, Study, Research, Chores, Work };

    }
}
