using HP.Application.Commands.Category;
using HP.Controllers;
using HP.Shared.Requests.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace HP.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : BaseApiController
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest request, CancellationToken token = default)
        {
            var cmd = new CreateCategoryCommand { Name = request.CategoryName, Desc = request.CategoryDesc, Type = request.CategoryType };
            return Ok(_mediator.Send(cmd));
        }
    }
}
