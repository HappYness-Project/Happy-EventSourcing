using AutoMapper;
namespace HP.Application
{
    public interface IApplicationService { }
    public abstract class ApplicationService : IApplicationService
    {
        //private readonly IInMemoryBus _inMemoryBus;
        protected readonly IMapper _mapper;
        //protected readonly IDomainNotificationHandler _notifications;
    }
}
