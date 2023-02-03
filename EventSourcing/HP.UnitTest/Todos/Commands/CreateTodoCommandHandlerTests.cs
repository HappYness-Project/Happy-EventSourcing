using HP.Application.Commands;
using HP.test;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using HP.Application.Commands.Todo;
using System;
using HP.Domain.Todos.Write;
using HP.Domain.People.Write;
using FluentAssertions;
using HP.Core.Commands;

namespace HP.UnitTest.Todos.Commands
{
    public class CreateTodoCommandHandlerTests : TestBase
    {
        private readonly Mock<ITodoAggregateRepository> _todoRepositoryMock;
        private readonly Mock<IPersonAggregateRepository> _personRepositoryMock;
        public CreateTodoCommandHandlerTests()
        {
            _todoRepositoryMock = new();
            _personRepositoryMock = new();
        }

        [Test]
        public async Task Handle_Should_ReturnFailureResult_UserNotExist()
        {
            // Arrange
            var cmd = new CreateTodoCommand(Guid.NewGuid(), "TodoTitle", "Valid");
            var handler = new CreateTodoCommandHandler(_eventProducer.Object, _todoRepositoryMock.Object, _personRepositoryMock.Object);
            //_todoRepositoryMock.Setup(x => x.Find(x=> x.))
            // Act 
            CommandResult result = await handler.Handle(cmd, default);

            // Asset
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("Error");
        }

        [Test]
        public async Task Handle_Should_ReturnSuccessResult_UserExist()
        {

        }
    }
}
