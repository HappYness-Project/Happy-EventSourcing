using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
