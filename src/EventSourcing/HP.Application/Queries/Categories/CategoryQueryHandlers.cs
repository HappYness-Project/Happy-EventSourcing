using AutoMapper;
using HP.Application.DTOs;
using HP.Domain;
using HP.Domain.Categories;
using MediatR;

namespace HP.Application.Queries
{
    public class CategoryQueryHandlers : BaseQueryHandler
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryQueryHandlers(IMapper mapper, ICategoryRepository categoryRepository) : base(mapper)
        {
        }
    }
}
