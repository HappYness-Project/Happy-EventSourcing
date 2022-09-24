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
    public class TodosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TodosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetTodos()));
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetTodo(string id, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodoById(id), token);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [HttpGet, Route("users/{id}", Name = "GetTodosByUser")]
        public async Task<IActionResult> GetTodosByUser(string id, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodoById(id), token);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [HttpPost("{personId}")]
        public async Task<IActionResult> Create([FromRoute]string personId, [FromBody] CreateTodoRequest request, CancellationToken token = default)
        {
            if (request == null)
                return BadRequest();

            var todo = await _mediator.Send(new CreateTodoCommand(personId, request.Title, request.TodoType, request.Description, request.Tags), token);
            return CreatedAtAction(nameof(GetTodo), new { Id = todo.TodoId }, todo);// await _mediator.Publish(cmd, cancellationToken);
        }

        [HttpPut("{todoId}/todoItem")]
        public async Task<IActionResult> CreateTodoItem([FromRoute]string todoId, [FromBody]CreateTodoItemRequest request, CancellationToken token = default)
        {
            if (request == null)
                return BadRequest();
            
            var todo = await _mediator.Send(new CreateTodoItemCommand(todoId, request.TodoTitle, request.TodoType, request.Description, request.Tags), token); 
            return Ok(todo);           
        }
        [HttpPut("{todoId}/todoItem")]
        public async Task<IActionResult> DeleteTodoItem([FromRoute]string todoId, [FromBody]CreateTodoItemRequest request, CancellationToken token = default)
        {
            if (request == null)
                return BadRequest();
            
            var todo = await _mediator.Send(new CreateTodoItemCommand(todoId, request.TodoTitle, request.TodoType, request.Description, request.Tags), token); 
            return Ok(todo);           
        }



        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody]UpdateTodoRequest todoRequest, CancellationToken token = default)
        {
            if (todoRequest == null)
                return BadRequest();

            var cmd = new UpdateTodoCommand(todoRequest.TodoId, todoRequest.TodoTotle, todoRequest.TodoDescription, todoRequest.Tags);
            return Ok(await _mediator.Send(cmd, token));
        }

        [HttpPatch("{todoId}/start")]
        public async Task<IActionResult> StartTodo([FromRoute]string todoId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new StartTodoCommand(todoId)));
        } 


        [HttpPatch("{todoId}/pending")]
        public async Task<IActionResult> PendingTodo([FromRoute]string todoId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new PendingTodoCommand(todoId)));
        } 


        [HttpPatch("{todoId}/stop")]
        public async Task<IActionResult> StopTodo([FromRoute]string todoId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new StartTodoCommand(todoId)));
        } 



        [Route("{todoId}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string todoId, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest("Todo id is null");

            return Ok(await _mediator.Send(new DeleteTodoCommand(todoId),token));
        }
    }
}
