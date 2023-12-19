using AutoMapper;
using HP.Application.DTOs;
using HP.Domain;
using HP.Domain.People.Read;
using HP.Domain.Todos.Read;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace HP.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.GoalType, opt => opt.MapFrom(src => src.GoalType));

            CreateMap<PersonDetails, PersonDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.PersonName))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.PersonType, opt => opt.MapFrom(src => src.PersonType));



            CreateMap<Todo, TodoBasicInfoDto>()
                .ForMember(dest => dest.TodoId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TodoTitle, opt => opt.MapFrom(src => src.Title));

            CreateMap<Domain.TodoItem, DTOs.TodoItemDto>();

            CreateMap<Todo, TodoDetailsDto>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.TodoId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TodoTitle, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.TodoType, opt => opt.MapFrom(src => src.TodoType.Name))
                .ForMember(dest => dest.SubTodos, opt => opt.MapFrom(src => src.SubTodos))
                .ForMember(dest => dest.TodoStatus, opt => opt.MapFrom(src => src.Status.Name));
            CreateMap<Todo, TodoBasicInfoDto>()
                .ForMember(dest => dest.TodoId, opt => opt.MapFrom(src => src.Id));

            CreateMap<TodoDetails, TodoBasicInfoDto>()
                .ForMember(dest => dest.TodoId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.TodoTitle, opt => opt.MapFrom(src => src.Title));
        }
    }
}
