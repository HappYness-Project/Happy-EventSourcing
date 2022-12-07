﻿using HP.Core.Commands;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record StopTodoCommand(string TodoId, string reason) : BaseCommand;
    public class StopTodoCommandHandler : IRequestHandler<StopTodoCommand, CommandResult>
    {
        private readonly ITodoRepository _repository;
        public StopTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(StopTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetActiveTodoById(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"Todo ID: {cmd.TodoId} does not exist.");

            todo.SetStatus(TodoStatus.Stop, cmd.reason);
            await _repository.UpdateAsync(todo);
            return new CommandResult(true, "Todo status is changed.", todo.Id);
        }
    }
}