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
        // Need to update Identity Service. 
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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodosByUser(string id, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodoByIdQuery(id));
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }


        [Route("{personId}/todos")]
        [HttpPost]
        public async Task<IActionResult> Create([FromRoute]string personId, [FromBody] CreateTodoDto todoDto, CancellationToken cancellationToken = default)
        {
            if (todoDto == null)
                return BadRequest();

            var cmd = new CreateTodoCommand(personId, todoDto.Title, todoDto.Description);
            var todo = await _mediator.Send(cmd);
            await _mediator.Publish(cmd, cancellationToken);
            return Ok(todo);
        }

        [Route("{personId}/Todos/{todoId}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string todoId, CancellationToken token = default)
        {
            var cmd = new DeleteTodoCommand(todoId);
            return Ok();
        }



    }
}
