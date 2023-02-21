using HP.Domain.Common;
using HP.Domain.Exceptions;

namespace HP.Domain
{
    public class PersonRole : Enumeration
    {
        public PersonRole(int id, string name) : base(id, name) { }
        public static PersonRole TBD => new(0, nameof(TBD).ToLowerInvariant());
        public static PersonRole Master => new(1, nameof(Master).ToLowerInvariant());
        public static PersonRole Admin => new(2, nameof(Admin).ToLowerInvariant());
        public static PersonRole Normal => new(3, nameof(Normal).ToLowerInvariant());
        public static PersonRole Guest => new(4, nameof(Guest).ToLowerInvariant());
        public static IEnumerable<PersonRole> List() => new[] { TBD, Master, Admin, Normal, Guest };
        public static PersonRole FromName(string name)
        {
            var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));
            if (state == null)
            {
                throw new PersonDomainException($"Possible PersonRole Inputs : {string.Join(",", List().Select(s => s.Name))})");
            }
            return state;
        }
    }
}
