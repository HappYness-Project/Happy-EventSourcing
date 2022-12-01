using HP.Api.Requests;
using HP.Application.Commands;
using HP.Application.Commands.Todo;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodo(string id, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodoById(id), token);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [HttpGet("{todoId}/TodoItems")]
        public async Task<IActionResult> GetTodoItemsByTodoId(string todoId, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetActiveTodoItemsByTodoId(todoId), token);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [HttpGet("{todoId}/TodoItems/{todoItemId}")]
        public async Task<IActionResult> GetTodoItemsByTodoItemId(string todoId, string todoItemId, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodoItemByTodoItemId(todoId, todoItemId), token);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [HttpGet("{todoId}/TodoItems/{TodoItemId}")]
        public async Task<IActionResult> GetCompletedTodoItemsByTodoId(string todoId, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetCompletedTodoItemsByTodoId(todoId), token);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetTodosByUser([FromRoute]string id, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodosByUserId(id), token);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [HttpPost("{personId}")]
        public async Task<IActionResult> Create(string personId, [FromBody] CreateTodoRequest request, CancellationToken token = default)
        {
            if (request == null)
                return BadRequest();

            var todo = await _mediator.Send(new CreateTodoCommand(personId, request.Title, request.TodoType, request.Description, request.StartDate, request.TargetEndDate, request.Tags), token);
            //return CreatedAtAction(nameof(GetTodo), new { Id = todo.TodoId }, todo);
            return Ok(todo);
        }
        [HttpPost("{todoId}/todoItem")]
        public async Task<IActionResult> CreateTodoItem(string todoId, [FromBody]CreateTodoItemRequest request, CancellationToken token = default)
        {
            if (request == null)
                return BadRequest();
            
            var todo = await _mediator.Send(new CreateTodoItemCommand(todoId, request.TodoTitle, request.TodoType, request.Description, request.Tags), token); 
            return Ok(todo);           
        }
        [HttpPut("{todoId}/todoItems/{todoItemId}")]
        public async Task<IActionResult> DeleteTodoItem(string todoId, string todoItemId, CancellationToken token = default)
        {
            if (todoId == null || todoItemId == null) 
                return BadRequest("TodoId or TodoItemId is null");
            
            var todo = await _mediator.Send(new DeleteTodoItemCommand(todoId, todoItemId), token); 
            return Ok(todo);           
        }
        [HttpPut("{todoId}")]
        public async Task<IActionResult> Update(string todoId, [FromBody]UpdateTodoRequest request, CancellationToken token = default)
        {
            if (request == null)
                return BadRequest();

            var cmd = new UpdateTodoCommand(request.TodoId, request.Title, request.Type, request.Description, request.Tags);
            return Ok(await _mediator.Send(cmd, token));
        }
        [HttpPut("UpdateStatusTodoItem")]
        public async Task<IActionResult> UpdateStatusTodoItem([FromBody]UpdateStatusTodoItemRequest request, CancellationToken token = default)
        {
            if (request == null)
                return BadRequest();
            var cmd = new UpdateTodoItemStatusCommand(request.TodoId, request.TodoItemId, request.NewStatus);
            return Ok(await _mediator.Send(cmd, token));
        }
        [HttpPatch("{todoId}/Activation")]
        public async Task<IActionResult> ActivateTodo([FromRoute]string todoId, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new ActivateTodoCommand(todoId), token));
        } 
        [HttpPatch("{todoId}/Deactivation")]
        public async Task<IActionResult> DeactivateTodo([FromRoute]string todoId, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new DeavtivateTodoCommand(todoId), token));
        } 
        [HttpPatch("{todoId}/start")]
        public async Task<IActionResult> StartTodo([FromRoute]string todoId, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new StartTodoCommand(todoId), token));
        } 
        [HttpPatch("{todoId}/pending")]
        public async Task<IActionResult> PendingTodo(string todoId, [FromBody]TodoStatusChangeRequest request, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new PendingTodoCommand(todoId), token));
        } 
        [HttpPatch("{todoId}/stop")]
        public async Task<IActionResult> StopTodo(string todoId, [FromBody]TodoStatusChangeRequest request, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new StopTodoCommand(todoId, request.reason), token));
        }
        [HttpPatch("{todoId}/complete")]
        public async Task<IActionResult> CompleteTodo(string todoId, [FromBody] TodoStatusChangeRequest request, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new CompleteTodoCommand(todoId), token));
        }
        [HttpDelete("{todoId}")]
        public async Task<IActionResult> Delete(string todoId, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest("Todo id is null");

            return Ok(await _mediator.Send(new DeleteTodoCommand(todoId),token));
        }
    }
}
