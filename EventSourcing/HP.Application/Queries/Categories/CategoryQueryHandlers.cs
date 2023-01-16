using AutoMapper;
using HP.Application.DTOs;
using HP.Domain;
using HP.Domain.Categories;
using HP.Domain.People.Write;
using MediatR;

namespace HP.Application.Queries
{
    public class CategoryQueryHandlers : BaseQueryHandler,
                                        IRequestHandler<GetCategories, IEnumerable<Category>>
    {
        private readonly IPersonAggregateRepository _personRepository;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryQueryHandlers(IMapper mapper, ICategoryRepository categoryRepository, IPersonAggregateRepository personRepository) : base(mapper)
        {
            _personRepository = personRepository;
        }

        public Task<IEnumerable<Category>> Handle(GetCategories request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
