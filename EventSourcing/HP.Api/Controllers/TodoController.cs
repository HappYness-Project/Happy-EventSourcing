using HP.Api.DTO;
using HP.Application.Commands;
using HP.Application.Handlers;
using HP.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {

        private readonly IMediator _mediator;
        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodoByIdQuery(id));
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoDto todoDto, CancellationToken cancellationToken = default)
        {
            if (todoDto == null)
                return BadRequest();

            var cmd = new CreateTodoCommand(todoDto.Title, todoDto.Description);
            var todo = await _mediator.Send(cmd);
            await _mediator.Publish(cmd, cancellationToken);
            return Ok(todo);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id, CancellationToken token = default)
        {
            var cmd = new DeleteTodoCommand(id);
            return Ok();
        }



    }
}
