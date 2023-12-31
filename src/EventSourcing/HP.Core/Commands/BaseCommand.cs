using MediatR;
namespace HP.Core.Commands;
public abstract record BaseCommand : IRequest<CommandResult> { }
