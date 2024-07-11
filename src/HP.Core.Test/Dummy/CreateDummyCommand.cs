using HP.Core.Commands;
using HP.Core.Common;
using MediatR;

namespace HP.Core.Test.Dummy;
public record class CreateDummyCommand(string Name) : BaseCommand { }

public class DummyCommandHandler : IRequestHandler<CreateDummyCommand, CommandResult>
{
    private IAggregateRepository<DummyAggregate> _dummyRepository;
    public DummyCommandHandler(IAggregateRepository<DummyAggregate> aggregateRepository)
        => _dummyRepository = aggregateRepository;
    public async Task<CommandResult> Handle(CreateDummyCommand cmd, CancellationToken cancellationToken)
    {
        var dummy = new DummyAggregate(cmd.Name, "TBD");
        await _dummyRepository.PersistAsync(dummy);
        return new CommandResult(true, "Successfully Dummy has been created.", dummy.Id.ToString());
    }
}


