using HP.Controllers;
using HP.Domain;
using HP.Shared.Requests.Todos;
using MediatR;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace HP.UnitTest.Todos
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
            var result = await controller.GetTodoItemsByTodoId(Guid.NewGuid());
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public async Task CreateTodo_Should_Create()
        {

            var mediator = new Mock<IMediator>();
            var controller = new TodosController(mediator.Object);
            var createRequest = new CreateTodoDto
            {
                Title = "Testing",
                Description = "Test",
                TargetStartDate = DateTime.Now,
                TodoType = TodoType.Research.ToString(),
                TargetEndDate = DateTime.Now,
            };
            var result = await controller.Create(Guid.NewGuid(), createRequest);
            Assert.That(result, Is.Not.Null);
        }
    }
}