using HP.Domain.Common;

namespace HP.Domain
{
    public class PersonRoleType : Enumeration
    {
        public PersonRoleType(int id, string name) : base(id, name)
        {
        }
        public static PersonRoleType Master => new(0, "Master");
        public static PersonRoleType Normal => new(1, "Admin");
        public static PersonRoleType Admin => new(2, "Normal");
    }
}
