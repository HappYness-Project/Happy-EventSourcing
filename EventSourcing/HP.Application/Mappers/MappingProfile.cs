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
            CreateMap<Person, PersonDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.GoalType, opt => opt.MapFrom( src => src.GoalType))
                .ForMember(dest => dest.AddressStr, opt => opt.MapFrom( src=> src.Address.ToString()));
            CreateMap<Todo, TodoBasicInfoDto>()
                .ForMember(dest => dest.TodoId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TodoTitle, opt => opt.MapFrom(src => src.Title));

            CreateMap<Domain.TodoItem, DTOs.TodoItemDto>();


            CreateMap<Todo, TodoDetailsDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.TodoId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TodoTitle, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.TodoType, opt => opt.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.SubTodos, opt => opt.MapFrom(src => src.SubTodos))
                .ForMember(dest => dest.TodoStatus, opt => opt.MapFrom(src => src.Status.Name));

            CreateMap<Todo, TodoBasicInfoDto>()
                .ForMember(dest => dest.TodoId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
