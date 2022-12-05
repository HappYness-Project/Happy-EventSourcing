using HP.Api.Requests;
using HP.Application.Commands;
using HP.Application.Commands.Person;
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
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IEnumerable<PersonDetailsDto>> Get()
        {
            return await _mediator.Send(new GetPersonList());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken token = default)
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
            
            var cmd = new CreatePersonCommand(request.PersonId, request.PersonType, request.GroupId);
            //var userId = await _domainMessageBroker.SendAsync(createUserCommand, CancellationToken.None);//TODO: Since it is a Create, I think it's desirable to use Publish command
            return Ok(_mediator.Send(cmd));
        }
        [HttpPut("{userid}")]
        public async Task<CommandResult> Update(string userid, [FromBody]UpdatePersonRequest request)
        {
            var result = await _mediator.Send(new UpdatePersonCommand(request.PersonId, request.PersonType, request.GroupId));
            return result;
        }
        [HttpPut("{userid}/Role")]
        public async Task UpdateRole(string userid, [FromBody]UpdateRoleRequest request)
        {
            await _mediator.Send(new UpdatePersonRoleCommand(userid, request.Role));
        }
        [HttpPut("{userid}/Group")]
        public async Task UpdateGroup(string userid, [FromBody]UpdateGroupIdRequest request)
        {
            await _mediator.Send(new UpdatePersonGroupCommand(userid, request.GroupId));
        }
    }
}
