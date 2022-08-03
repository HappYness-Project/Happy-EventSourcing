using HP.Api.DTO;
using HP.Application.Commands;
using HP.Application.Queries.Person;
using HP.Domain.Person;
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
        public async Task<IEnumerable<Person>> Get()
        {
            return await _mediator.Send(new GetPersonListQuery());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken token = default)
        {
            var person = await _mediator.Send(new GetPersonByIdQuery(id)); 
            if(person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreatePersonDto personDto, CancellationToken cancellationToken = default)
        {
            if (personDto == null)
                return BadRequest();
            
            var cmd = new CreatePersonCommand(personDto.FirstName, personDto.LastName, personDto.Address, personDto.UserId);
            // Message Broker call? Should we need to do in both? 
            //var userId = await _domainMessageBroker.SendAsync(createUserCommand, CancellationToken.None);
            //TODO: Since it is a Create, I think it's desirable to use Publish command .  
            await _mediator.Publish(cmd, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<Person> Update(string id, [FromBody]UpdatePersonDto personDto)
        {
            return await _mediator.Send(new UpdatePersonCommand(id, personDto.FirstName, personDto.LastName,personDto.Email));
        }

    }
}
