using HP.Domain.Common;
using HP.Domain.Exceptions;

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


        public static TodoType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new TodoDomainException($"Possible values for OrderStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
        public static TodoType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new TodoDomainException($"Possible values for OrderStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

    }
}
