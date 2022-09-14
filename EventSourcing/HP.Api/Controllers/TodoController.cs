using HP.Api.Requests;
using HP.Application.Commands;
using HP.Application.Handlers;
using HP.Application.Queries.Todos;
using HP.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HP.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetTodos()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodoById(id));
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetTodosByUser(string userId, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodosByUserId(userId));
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [HttpPost("{personId}/todos")]
        public async Task<IActionResult> Create([FromRoute]string personId, [FromBody] CreateTodoRequest createTodoRequest, CancellationToken cancellationToken = default)
        {
            if (createTodoRequest == null)
                return BadRequest();

            //TODO :  Should I check from here if person exists?


            var cmd = new CreateTodoCommand(personId, createTodoRequest.Title, createTodoRequest.Description);
            var todo = await _mediator.Send(cmd);
            return CreatedAtAction("GetTodo", new { Title = cmd.todoTitle }, cmd);// await _mediator.Publish(cmd, cancellationToken);
        }

        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody]UpdateTodoRequest todoRequest, CancellationToken cancellationToken = default)
        {
            if (todoRequest == null)
                return BadRequest();

            var cmd = new UpdateTodoCommand(todoRequest.TodoId, todoRequest.TodoTotle, todoRequest.TodoDescription, todoRequest.Tags);
            return Ok(await _mediator.Send(cmd));
        }

        [HttpPut("{todoId}/start")]
        public async Task<IActionResult> StartTodo([FromRoute]string todoId, CancellationToken cancellationToken = default)
        {
            // Todo User Authentication required.
            if (string.IsNullOrEmpty(todoId))
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new StartTodoCommand(todoId)));
        } 

        [Route("/Todos/{todoId}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string todoId, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest("Todo id is null");

            var cmd = new DeleteTodoCommand(todoId);
            return Ok(await _mediator.Send(cmd));
        }



    }
}
