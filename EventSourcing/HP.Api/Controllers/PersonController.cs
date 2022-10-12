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
            var person = await _mediator.Send(new GetPersonById(id)); 
            if(person == null)
                return NotFound();
            
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreatePersonRequest personDto, CancellationToken token = default)
        {
            if (personDto == null)
                return BadRequest();
            
            var cmd = new CreatePersonCommand(personDto.FirstName, personDto.LastName, personDto.Address, personDto.Email, personDto.UserId);
            //var userId = await _domainMessageBroker.SendAsync(createUserCommand, CancellationToken.None);//TODO: Since it is a Create, I think it's desirable to use Publish command
            //return Ok(await _mediator.Send(cmd, token));
            return Ok(await _mediator.Publish(cmd, token));
        }

        [HttpPut("{userid}")]
        public async Task<bool> Update(string userid, [FromBody]UpdatePersonRequest request)
        {
            return await _mediator.Send(new UpdatePersonCommand(userid, request.FirstName, request.LastName,request.Email));
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
