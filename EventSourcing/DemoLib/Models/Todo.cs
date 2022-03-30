using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }   
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}
