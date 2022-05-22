using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.Events
{
    public class DomainNotificationBase<T> 
    {
        public string Id { get; set; }
        public string Type { get; set; }
    }
}
