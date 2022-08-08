using MediatR;
namespace HP.Application.Commands
{
    public interface ICommand : IRequest { string Id { get; } }
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        string Id { get; }
    }

    public abstract record CommandBase<TResult> : ICommand<TResult>
    {
        public string Id { get; }

        protected CommandBase()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        protected CommandBase(string id)
        {
            this.Id = id;
        }
    }
}
