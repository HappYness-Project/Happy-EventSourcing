using HP.Api.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace HP.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request, CancellationToken token = default)
        {
            //var cmd = new CreateCategoryCommand(personDto.FirstName, personDto.LastName, personDto.Address, personDto.Email, personDto.UserId);
            //return Ok(_mediator.Send(cmd));
            return Ok();
        }
    }
}
