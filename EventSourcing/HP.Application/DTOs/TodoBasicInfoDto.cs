using HP.Domain.Todos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.DTOs
{
    public class TodoBasicInfoDto
    {
        public string UserId { get; set; }
        public string TodoTitle { get; set; }
        public string TodoType { get; set; }
        public TodoStatus TodoStatus { get; set; }
    }
}
