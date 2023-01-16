using HP.Domain.Common;

namespace HP.Domain
{
    public class PersonRole : Enumeration
    {
        public PersonRole(int id, string name) : base(id, name)
        {
        }
        public static PersonRole Master => new(0, "Master");
        public static PersonRole Normal => new(1, "Admin");
        public static PersonRole Admin => new(2, "Normal");
        public static PersonRole TBD => new(2, "TBD");
    }
}
