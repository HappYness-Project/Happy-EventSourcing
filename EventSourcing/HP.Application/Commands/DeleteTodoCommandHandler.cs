﻿using HP.Domain;
using HP.Domain.Todos;
using MediatR;
using System.Linq.Expressions;

namespace HP.Application.Commands
{
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
    {
        private readonly ITodoRepository _repository;
        public DeleteTodoCommandHandler(ITodoRepository todoRepository)
        {
            this._repository = todoRepository;
        }
        public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<Todo, bool>> expr = x => x.Id == request.id;
            await _repository.DeleteOneAsync(expr);
            //TODO : How can we make it to return true or false? or should we need to do it?
            return true;
        }
    }
}
