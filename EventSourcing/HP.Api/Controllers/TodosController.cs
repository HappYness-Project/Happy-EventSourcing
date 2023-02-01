using HP.Api.Requests;
using HP.Application.Commands.Todo;
using HP.Application.DTOs;
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
        public async Task<IActionResult> GetTodo(Guid id, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodoById(id), token);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [HttpGet("{todoId}/TodoItems")]
        public async Task<IActionResult> GetTodoItemsByTodoId(Guid todoId, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetActiveTodoItemsByTodoId(todoId), token);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [HttpGet("{todoId}/TodoItems/{todoItemId}")]
        public async Task<IActionResult> GetTodoItemByTodoItemId(Guid todoId, Guid todoItemId, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodoItemByTodoItemId(todoId, todoItemId), token);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [HttpGet("{todoId}/TodoItems/status/{status}")]
        public async Task<IActionResult> GetTodoItemsByStatus(Guid todoId, string status, CancellationToken token = default)
        {
            IEnumerable<TodoItemDto> items = null;
            if (status == "complete")
                items = await _mediator.Send(new GetCompletedTodoItemsByTodoId(todoId), token);
            else if (status == "pending")
                items = await _mediator.Send(new GetPendingTodoItemsByTodoId(todoId), token);
            else if (status == "start")
                items = await _mediator.Send(new GetStartedTodoItemsByTodoId(todoId), token);
            else if (status == "stop")
                items = await _mediator.Send(new GetStoppedTodoItemsByTodoId(todoId), token);

            if (items == null)
                return NotFound();

            return Ok(items);
        }
        [HttpGet("People/{PersonName}")]
        public async Task<IActionResult> GetTodosByUser([FromRoute] string personName, CancellationToken token = default)
        {
            if (personName == null)
                return BadRequest();

            var todo = await _mediator.Send(new GetTodosByPersonName(personName), token);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [IgnoreAntiforgeryToken]
        [HttpPost("{personId}")]
        public async Task<IActionResult> Create(Guid personId, [FromBody] CreateTodoDto request, CancellationToken token = default)
        {
            if (Guid.Empty == personId)
                return BadRequest($"TodoId is null.");

            var todo = await _mediator.Send(new CreateTodoCommand(personId, request.Title, request.TodoType, request.Description, request.TargetStartDate, request.TargetEndDate, null), token);
            //return CreatedAtAction(nameof(GetTodo), new { Id = todo.TodoId }, todo);
            return Ok(todo);
        }
        [HttpPost("{todoId}/todoItems")]
        public async Task<IActionResult> CreateTodoItem(Guid todoId, [FromBody] CreateTodoItemDto request, CancellationToken token = default)
        {
            if (Guid.Empty == todoId)
                return BadRequest();

            var todo = await _mediator.Send(new CreateTodoItemCommand(todoId, request.TodoTitle, request.TodoType, request.Description, null), token);
            return Ok(todo);
        }
        [HttpDelete("{todoId}/todoItems/{todoItemId}")]
        public async Task<IActionResult> DeleteTodoItem(Guid todoId, Guid todoItemId, CancellationToken token = default)
        {
            if (Guid.Empty == todoId || Guid.Empty == todoItemId)
                return BadRequest();

            var todo = await _mediator.Send(new DeleteTodoItemCommand(todoId, todoItemId), token);
            return Ok(todo);
        }
        [HttpPut("{todoId}")]
        public async Task<IActionResult> Update(Guid todoId, [FromBody] UpdateTodoDto request, CancellationToken token = default)
        {
            if (Guid.Empty == todoId)
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new UpdateTodoCommand(todoId, request.TodoTitle, request.TodoType, request.Description, null, request.TargetStartDate, request.TargetEndDate), token));
        }
        [HttpPatch("{todoId}/Activation")]
        public async Task<IActionResult> ActivateTodo([FromRoute] Guid todoId, CancellationToken token = default)
        {
            if (Guid.Empty == todoId)
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new ActivateTodoCommand(todoId), token));
        }
        [HttpPatch("{todoId}/Deactivation")]
        public async Task<IActionResult> DeactivateTodo([FromRoute] Guid todoId, CancellationToken token = default)
        {
            if (Guid.Empty == todoId)
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new DeavtivateTodoCommand(todoId), token));
        }
        [HttpPatch("{todoId}/start")]
        public async Task<IActionResult> StartTodo([FromRoute] Guid todoId, CancellationToken token = default)
        {
            if (Guid.Empty == todoId)
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new StartTodoCommand(todoId), token));
        }
        [HttpPatch("{todoId}/pending")]
        public async Task<IActionResult> PendingTodo(Guid todoId, [FromBody] TodoStatusChangeRequest request, CancellationToken token = default)
        {
            if (Guid.Empty == todoId)
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new PendingTodoCommand(todoId), token));
        }
        [HttpPatch("{todoId}/stop")]
        public async Task<IActionResult> StopTodo(Guid todoId, [FromBody] TodoStatusChangeRequest request, CancellationToken token = default)
        {
            if (Guid.Empty == todoId)
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new StopTodoCommand(todoId, request.reason), token));
        }
        [HttpPatch("{todoId}/complete")]
        public async Task<IActionResult> CompleteTodo(Guid todoId, [FromBody] TodoStatusChangeRequest request, CancellationToken token = default)
        {
            if (Guid.Empty == todoId)
                return BadRequest($"TodoId is null.");

            return Ok(await _mediator.Send(new CompleteTodoCommand(todoId), token));
        }

        [HttpPut("{todoId}/todoItems/{todoItemId}")]
        public async Task<IActionResult> UpdateTodoItem(Guid todoId, Guid todoItemId, [FromBody]UpdateTodoItemDto todoItem, CancellationToken token = default)
        {
            if (Guid.Empty == todoId)
                return BadRequest($"TodoId is null.");

            var cmd = new UpdateTodoItemCommand(todoId, todoItemId, todoItem.TodoTitle, todoItem.Description, todoItem.TodoType);
            return Ok(await _mediator.Send(cmd, token));
        }

        [HttpPut("{todoId}/todoItems/{todoItemId}/status")]
        public async Task<IActionResult> UpdateTodoItemStatus(Guid todoId, Guid todoItemId, [FromBody]UpdateTodoStatusDto dto, CancellationToken token = default)
        {
            if (todoId == null || todoItemId == null)
                return BadRequest();

            var cmd = new UpdateTodoItemStatusCommand(todoId, todoItemId, dto.Status);
            return Ok(await _mediator.Send(cmd, token));
        }
        [HttpDelete("{todoId}")]
        public async Task<IActionResult> Delete(Guid todoId, CancellationToken token = default)
        {
            if (Guid.Empty == todoId)
                return BadRequest("Todo id is null");

            return Ok(await _mediator.Send(new DeleteTodoCommand(todoId), token));
        }
    }
}
