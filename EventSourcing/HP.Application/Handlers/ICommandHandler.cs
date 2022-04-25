using HP.Application.Commands;
using MediatR;

namespace HP.Application.Handlers
{
    public interface ICommandHandler<in T> : IRequestHandler<T> where T : ICommand
    {
    }
}
//Please read this.
//https://deviq.com/domain-driven-design/aggregate-pattern
//https://github.com/gautema/CQRSlite/blob/79b81005e124663a142afa1e170d50df75105467/Framework/CQRSlite/Events/IEvent.cs
//https://github.com/revoframework/Revo/blob/a3ee51494bd654d737f99295e28965eea736d4af/Revo.Core/Commands/CommandContextStack.cs
//https://github.com/SneakyPeet/EasyEventSourcing/blob/449b17ae2dd475da54580121bca7f435fa5c299d/src/EasyEventSourcing.EventSourcing/Domain/EventStream.cs#L8
//https://codeopinion.com/category/event-sourcing/
//https://codeopinion.com/publishing-events-from-crud-or-commands/
//https://codeopinion.com/event-based-architecture-what-do-you-mean-by-event/
//https://github.com/kgrzybek/sample-dotnet-core-cqrs-api/blob/2f00e194e72e9288dddd69af499fc97920cba86e/src/SampleProject.Infrastructure/Domain/Customers/CustomerRepository.cs