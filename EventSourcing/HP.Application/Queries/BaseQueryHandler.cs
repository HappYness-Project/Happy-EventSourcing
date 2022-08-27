using AutoMapper;

namespace HP.Application.Queries
{
    public abstract class BaseQueryHandler
    {
        protected readonly IMapper _mapper;
        protected BaseQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
