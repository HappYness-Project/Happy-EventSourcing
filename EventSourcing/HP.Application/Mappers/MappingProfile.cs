using AutoMapper;
using HP.Application.DTOs;
using HP.Domain;
using HP.Domain.People.Read;
using HP.Domain.Todos.Read;

namespace HP.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.PersonName))
                .ForMember(dest => dest.GoalType, opt => opt.MapFrom(src => src.GoalType));

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
