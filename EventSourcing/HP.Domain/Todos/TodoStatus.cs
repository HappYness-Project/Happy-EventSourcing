using HP.Domain.Common;
using HP.Domain.Exceptions;

namespace HP.Domain
{
    public class TodoStatus : Enumeration
    {
        public TodoStatus(int id, string name) : base(id, name){ }
        public static TodoStatus NotDefined => new(0, nameof(NotDefined).ToLowerInvariant());
        public static TodoStatus Accept => new(1, nameof(Accept).ToLowerInvariant());
        public static TodoStatus Start => new(2, nameof(Start).ToLowerInvariant());
        public static TodoStatus Complete => new(3, nameof(Complete).ToLowerInvariant());
        public static TodoStatus Stop => new(4, nameof(Stop).ToLowerInvariant());
        public static TodoStatus WontDo => new(5, nameof(WontDo).ToLowerInvariant());
        public static TodoStatus Pending => new(6, nameof(Pending).ToLowerInvariant());
        public static IEnumerable<TodoStatus> List() => new[] { NotDefined, Accept, Start, Complete, Stop, WontDo, Pending };

        public static TodoStatus FromName(string name)
        {
            var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));
            if(state == null)
            {
                throw new TodoDomainException($"Possible Todo Statuses : {string.Join(",", List().Select(s => s.Name))})");
            }
            return state;
        }


    }
}
