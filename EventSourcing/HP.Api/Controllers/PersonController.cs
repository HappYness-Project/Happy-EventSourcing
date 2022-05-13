using HP.Api.DTO;
using HP.Application.Commands;
using HP.Application.Queries;
using HP.Domain;
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
        //[ProducesResponseType(typeof(UserId))]
        public async Task<IActionResult> Create([FromBody]CreatePersonDto personDto, CancellationToken cancellationToken = default)
        {
            if (personDto == null)
                return BadRequest();
            
            var cmd = new InsertPersonCommand(personDto.FirstName, personDto.LastName);
            //var createUserCommand = new CreateUserCommand(new Email(postUserHttpRequest?.Email ?? string.Empty));
            //var userId = await _domainMessageBroker.SendAsync(createUserCommand, CancellationToken.None);
            var person = await _mediator.Send(new InsertPersonCommand(personDto.FirstName, personDto.LastName));
            //TODO: Since it is a Create, I think it's desirable to use Publish command . 
            await _mediator.Publish(cmd, cancellationToken);
            return Ok(person);
        }

        //[HttpPut("{id}")]
        //public async Task<PersonModel> Update(int id, PersonModel value)
        //{
        //    return await _mediator.Send(new UpdatePersonCommand(value.))
        //}


    }
}
