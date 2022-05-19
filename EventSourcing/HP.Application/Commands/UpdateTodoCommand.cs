using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.Commands
{
    public record UpdateTodoCommand(string TodoId, string Title, string Description, string[] Tags) : IRequest<bool>;
}
