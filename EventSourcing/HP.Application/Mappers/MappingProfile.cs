using AutoMapper;
using HP.Application.DTOs;
using HP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDetailsDto>();
            CreateMap<Todo, TodoBasicInfoDto>();
        }
    }
}
