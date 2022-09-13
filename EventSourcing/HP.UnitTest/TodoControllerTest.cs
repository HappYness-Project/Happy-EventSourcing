using HP.Controllers;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HP.test
{
    public class TodoControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }



        [Test]
        public async Task GetTodosByUser_Return_User()
        {

            var mediator = new Mock<IMediator>();
            var controller = new TodoController(mediator.Object);
            var result = await controller.GetTodosByUser("hyunbin7303");
            Assert.That(result, Is.Not.Null);    
        }

    }
}