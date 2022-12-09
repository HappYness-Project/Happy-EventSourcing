using HP.Api.Requests;
using HP.Application.Commands.Todo;
using HP.Application.Queries.Todos;
using HP.Shared.Requests.Todos;
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
        public async Task<IActionResult> GetTodoItemByTodoItemId(string todoId, string todoItemId, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodoItemByTodoItemId(todoId, todoItemId), token);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [HttpGet("{todoId}/TodoItems/completedItem")]
        public async Task<IActionResult> GetCompletedTodoItemsByTodoId(string todoId, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetCompletedTodoItemsByTodoId(todoId), token);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetTodosByUser([FromRoute] string id, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodosByUserId(id), token);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [HttpPost("{personId}")]
        public async Task<IActionResult> Create(string personId, [FromBody] CreateTodoDto request, CancellationToken token = default)
        {
            if (request == null)
                return BadRequest();

            var todo = await _mediator.Send(new CreateTodoCommand(personId, request.Title, request.TodoType, request.Description, request.StartDate, request.TargetEndDate, request.Tags), token);
            //return CreatedAtAction(nameof(GetTodo), new { Id = todo.TodoId }, todo);
            return Ok(todo);
        }
        [HttpPost("{todoId}/todoItem")]
        public async Task<IActionResult> CreateTodoItem(string todoId, [FromBody] CreateTodoItemDto request, CancellationToken token = default)
        {
            if (request == null)
                return BadRequest();

            var todo = await _mediator.Send(new CreateTodoItemCommand(todoId, request.TodoTitle, request.TodoType, request.Description, null), token);
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
        public async Task<IActionResult> Update(string todoId, [FromBody] UpdateTodoDto request, CancellationToken token = default)
        {
            if (request == null)
                return BadRequest();

            var cmd = new UpdateTodoCommand(request.TodoId, request.TodoTitle, request.TodoType, request.Description, null);
            return Ok(await _mediator.Send(cmd, token));
        }
        [HttpPatch("{todoId}/Activation")]
        public async Task<IActionResult> ActivateTodo([FromRoute] string todoId, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new ActivateTodoCommand(todoId), token));
        }
        [HttpPatch("{todoId}/Deactivation")]
        public async Task<IActionResult> DeactivateTodo([FromRoute] string todoId, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new DeavtivateTodoCommand(todoId), token));
        }
        [HttpPatch("{todoId}/start")]
        public async Task<IActionResult> StartTodo([FromRoute] string todoId, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new StartTodoCommand(todoId), token));
        }
        [HttpPatch("{todoId}/pending")]
        public async Task<IActionResult> PendingTodo(string todoId, [FromBody] TodoStatusChangeRequest request, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new PendingTodoCommand(todoId), token));
        }
        [HttpPatch("{todoId}/stop")]
        public async Task<IActionResult> StopTodo(string todoId, [FromBody] TodoStatusChangeRequest request, CancellationToken token = default)
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

        [HttpPut("{todoId}/todoItems/{todoItemId}")]
        public async Task<IActionResult> UpdateTodoItem(string todoId, string todoItemId, [FromBody]UpdateTodoDto todoItem, CancellationToken token = default)
        {
            if (todoId == null || todoItemId == null)
                return BadRequest();

            var cmd = new UpdateTodoItemCommand(todoId, todoItemId, todoItem.TodoTitle, todoItem.Description, todoItem.TodoType);
            return Ok(await _mediator.Send(cmd, token));
        }

        [HttpPut("{todoId}/todoItems/{todoItemId}/status")]
        public async Task<IActionResult> UpdateTodoItemStatus(string todoId, string todoItemId, [FromBody]UpdateTodoStatusDto dto, CancellationToken token = default)
        {
            if (todoId == null || todoItemId == null)
                return BadRequest();

            var cmd = new UpdateTodoItemStatusCommand(todoId, todoItemId, dto.Status);
            return Ok(await _mediator.Send(cmd, token));
        }
        [HttpDelete("{todoId}")]
        public async Task<IActionResult> Delete(string todoId, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(todoId))
                return BadRequest("Todo id is null");

            return Ok(await _mediator.Send(new DeleteTodoCommand(todoId), token));
        }
    }
}
