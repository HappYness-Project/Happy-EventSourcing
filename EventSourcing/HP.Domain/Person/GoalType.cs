using HP.Domain.Common;
using HP.Domain.Exceptions;
namespace HP.Domain
{
    public class GoalType : Enumeration
    {
        public GoalType(int id, string name) : base(id, name) { }
        public static GoalType NotDefined => new(0, nameof(NotDefined).ToLowerInvariant());
        public static GoalType Beginner => new(1, nameof(Beginner).ToLowerInvariant());
        public static GoalType Intermediate => new(2, nameof(Intermediate).ToLowerInvariant());
        public static GoalType Expert => new(3, nameof(Expert).ToLowerInvariant());
        public static IEnumerable<GoalType> List() => new[] { NotDefined, Beginner, Intermediate, Expert };
        public static GoalType FromName(string name)
        {
            var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));
            if (state == null)  
            {
                throw new PersonDomainException($"Possible GoalType Inputs : {string.Join(",", List().Select(s => s.Name))})");
            }
            return state;
        }
    }
}
