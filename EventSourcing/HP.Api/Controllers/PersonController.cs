using HP.Api.Requests;
using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Application.Queries;
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
            var person = await _mediator.Send(new GetPersonList()); 
            if(person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreatePersonRequest personDto, CancellationToken cancellationToken = default)
        {
            if (personDto == null)
                return BadRequest();
            
            var cmd = new CreatePersonCommand(personDto.FirstName, personDto.LastName, personDto.Address, personDto.Email, personDto.UserId);
            //var userId = await _domainMessageBroker.SendAsync(createUserCommand, CancellationToken.None);
            //TODO: Since it is a Create, I think it's desirable to use Publish command .  
            await _mediator.Send(cmd, cancellationToken);//await _mediator.Publish(cmd, cancellationToken);
            return Ok();
        }

        [HttpPut("{userid}")]
        public async Task<bool> Update(string userid, [FromBody]UpdatePersonRequest request)
        {
            return await _mediator.Send(new UpdatePersonCommand(userid, request.FirstName, request.LastName,request.Email));
        }
        [HttpPut("Role/{userid}")]
        public async Task Update(string userid, [FromBody]UpdateRoleRequest request)
        {
            await _mediator.Send(new UpdatePersonRoleCommand(userid, request.Role));// Need to be updated to Publish.
        }
    }
}
