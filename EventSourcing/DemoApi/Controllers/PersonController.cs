using DemoLib.Commands;
using DemoLib.Models;
using DemoLib.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
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
        public async Task<List<Person>> Get()
        {
            return await _mediator.Send(new GetPersonListQuery());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken token = default)
        {
            var person = await _mediator.Send(new GetPersonByIdQuery(id)); 
            if(person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public async Task<Person> Post([FromBody]Person value)
        {
            return await _mediator.Send(new InsertPersonCommand(value.FirstName, value.LastName));
        }

        //[HttpPut("{id}")]
        //public async Task<PersonModel> Update(int id, PersonModel value)
        //{
        //    return await _mediator.Send(new UpdatePersonCommand(value.))
        //}

    }
}
