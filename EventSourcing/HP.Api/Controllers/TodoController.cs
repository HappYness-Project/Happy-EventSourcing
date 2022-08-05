using HP.Api.Requests;
using HP.Application.Commands;
using HP.Application.Handlers;
using HP.Application.Queries;
using HP.Domain.Todos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HP.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository repository;
        // Need to update Identity Service. 
        private readonly IMediator _mediator;
        public TodoController(ITodoRepository todoRepository, IMediator mediator)
        {
            repository = todoRepository;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodoById(id));
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetTodosByUser(string id, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodoById(id));
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }
        [HttpPost("{personId}/todos")]
        public async Task<IActionResult> Create([FromRoute]string personId, [FromBody] CreateTodoRequest createTodoRequest, CancellationToken cancellationToken = default)
        {
            if (createTodoRequest == null)
                return BadRequest();

            var cmd = new CreateTodoCommand(personId, createTodoRequest.Title, createTodoRequest.Description);
            await _mediator.Publish(cmd, cancellationToken);
            return CreatedAtAction("GetTodo", new { Title = cmd.TodoTitle }, cmd);
        }

        [HttpPut("")]
        public async Task<IActionResult> Update([FromRoute] string personId, [FromBody]UpdateTodoRequest todoRequest, CancellationToken cancellationToken = default)
        {
            var cmd = new UpdateTodoCommand(todoRequest.TodoId, todoRequest.TodoTotle, todoRequest.TodoDescription, todoRequest.Tags);
            //TODO Update info.
            return Ok();
        }


        [HttpPut("todos/{todoId}/start")]
        public async Task<IActionResult> StartTodo([FromRoute]string todoId, CancellationToken cancellationToken = default)
        {
            // TODO Getting User Identity from here. 
            var temp_userId = "hyunbin7303";
            var cmd = new StartTodoCommand(temp_userId, todoId);
            var todo = await _mediator.Send(cmd);
            return Ok(todo);
        } 



        [Route("/Todos/{todoId}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string todoId, CancellationToken token = default)
        {
            var cmd = new DeleteTodoCommand(todoId);
            var todo = await _mediator.Send(cmd);
            return Ok();
        }



    }
}
