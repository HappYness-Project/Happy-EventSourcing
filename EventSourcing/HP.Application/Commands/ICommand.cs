using MediatR;
namespace HP.Application.Commands
{
    public abstract record BaseCommand : IRequest<CommandResult> { }

}
