﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.Commands
{
   public record CancelTodoCommand(string todoId) : IRequest<bool>;
}