using HP.Domain.Common;
using HP.Domain.Exceptions;
namespace HP.Domain
{
    public class GoalType : Enumeration
    {
        public GoalType(int id, string name) : base(id, name) { }
        public static GoalType TBD => new(0, nameof(TBD).ToLowerInvariant());
        public static GoalType Beginner => new(1, nameof(Beginner).ToLowerInvariant());
        public static GoalType Intermediate => new(2, nameof(Intermediate).ToLowerInvariant());
        public static GoalType Expert => new(3, nameof(Expert).ToLowerInvariant());
        public static GoalType ShortTerm => new(4, nameof(ShortTerm).ToLowerInvariant());
        public static GoalType LongTerm => new(5, nameof(LongTerm).ToLowerInvariant());
        public static GoalType TimeBound => new(6, nameof(TimeBound).ToLowerInvariant());
        public static IEnumerable<GoalType> List() => new[] { TBD, Beginner, Intermediate, Expert, ShortTerm, LongTerm, TimeBound };
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
