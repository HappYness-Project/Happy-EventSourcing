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
            var controller = new TodosController(mediator.Object);
            var result = await controller.GetTodosByUser("hyunbin7303");
            Assert.That(result, Is.Not.Null);    
        }
        [Test]
        public async Task GetTodoItemsByTodoId_Return_GetSomeTodoItems()
        {

            var mediator = new Mock<IMediator>();
            var controller = new TodosController(mediator.Object);
            var result = await controller.GetTodoItemsByTodoId("6371a9b24337c5e8fcb86bf1");
            Assert.That(result, Is.Not.Null);
        }
    }
}