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

        [HttpGet, Route("{id}", Name = "GetTodo")]
        public async Task<IActionResult> Get(string id, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodoById(id), token);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
<<<<<<< HEAD:EventSourcing/HP.Api/Controllers/TodosController.cs
        [HttpGet, Route("users/{id}", Name = "GetTodosByUser")]
        public async Task<IActionResult> GetTodosByUser(string id, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodoById(id), token);
=======
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetTodosByUser(string userId, CancellationToken token = default)
        {
            var todo = await _mediator.Send(new GetTodosByUserId(userId));
>>>>>>> 0bc70ef680e150468d640d093d814c585a5f02d0:EventSourcing/HP.Api/Controllers/TodoController.cs
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
<<<<<<< HEAD:EventSourcing/HP.Api/Controllers/TodosController.cs
        [HttpPost("{UserName}/todos")]
        public async Task<IActionResult> Create([FromRoute]string UserName, [FromBody] CreateTodoRequest request, CancellationToken cancellationToken = default)
=======
        [HttpPost("{personId}")]
        public async Task<IActionResult> Create([FromRoute]string personId, [FromBody] CreateTodoRequest request, CancellationToken cancellationToken = default)
>>>>>>> 0bc70ef680e150468d640d093d814c585a5f02d0:EventSourcing/HP.Api/Controllers/TodoController.cs
        {
            if (request == null)
                return BadRequest();

<<<<<<< HEAD:EventSourcing/HP.Api/Controllers/TodosController.cs
            var todo = await _mediator.Send(new CreateTodoCommand(UserName, request.Title, request.TodoType, request.Description, request.Tags));
        //    return CreatedAtAction($"GetTodo", new { Title = todo.TodoTitle }, todo);// await _mediator.Publish(cmd, cancellationToken);
            return Ok(todo);
=======
            var cmd = new CreateTodoCommand(personId, request.Title, request.TodoType, request.Description, request.Tag);
            var todo = await _mediator.Send(cmd);
            return CreatedAtAction("GetTodo", new { Title = cmd.todoTitle }, cmd);// await _mediator.Publish(cmd, cancellationToken);
>>>>>>> 0bc70ef680e150468d640d093d814c585a5f02d0:EventSourcing/HP.Api/Controllers/TodoController.cs
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
