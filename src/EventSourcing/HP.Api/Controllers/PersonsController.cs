using HP.Application.Commands.Persons;
using HP.Application.DTOs;
using HP.Application.Queries;
using HP.Core.Commands;
using HP.Shared.Requests.People;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IEnumerable<PersonDetailsDto>> Get()
        {
            return await _mediator.Send(new GetPersonList());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken token = default)
        {
            var person = await _mediator.Send(new GetPersonById(id)); 
            if(person == null)
                return NotFound();
            
            return Ok(person);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreatePersonRequest request, CancellationToken token = default)
        {
            if (request == null)
                return BadRequest();

            return Ok(await _mediator.Send(new CreatePersonCommand(request.PersonName, request.PersonType, request.GroupId)));
        }
        [HttpPut("{personId}")]
        public async Task<CommandResult> Update(Guid personId, [FromBody]UpdatePersonRequest request)
        {
            var result = await _mediator.Send(new UpdatePersonCommand(personId, request.PersonType, request.GoalType, request.GroupId));
            return result;
        }
        [HttpPut("{personId}/Role")]
        public async Task UpdateRole(Guid personId, [FromBody]UpdateRoleRequest request)
        {
            await _mediator.Send(new UpdatePersonRoleCommand(personId, request.Role));
        }
        [HttpPut("{personId}/Group")]
        public async Task UpdateGroup(Guid personId, [FromBody]UpdateGroupIdRequest request)
        {
            await _mediator.Send(new UpdatePersonGroupCommand(personId, request.GroupId));
        }
    }
}
