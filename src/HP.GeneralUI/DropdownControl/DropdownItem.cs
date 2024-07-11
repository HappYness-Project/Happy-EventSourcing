using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.GeneralUI.DropdownControl
{
    public class DropdownItem<T>
    {
        public string DisplayText { get; set; }
        public T ItemObject { get; set; }
    }
}
