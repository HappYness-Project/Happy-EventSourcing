using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Shared.Contacts
{
    public interface ICurrentUserService
    {
        User CurrentUser { get; set; }
    }
}
