using HP.Domain.Todos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.Queries
{
    public record GetTodoByIdQuery(int Id) : IRequest<Todo>;

}
