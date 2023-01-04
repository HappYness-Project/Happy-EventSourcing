using HP.Application.Commands;
using HP.Domain;
using HP.test;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;
using HP.Application.DTOs;
using HP.Application.Commands.Todo;
using System;

namespace HP.UnitTest.Todos.Commands
{
    public class CreateTodoCommandHandlerTests : TestBase
    {
        private readonly Mock<ITodoRepository> _todoRepositoryMock;
        private readonly Mock<IPersonRepository> _personRepositoryMock;

        public CreateTodoCommandHandlerTests()
        {
            _todoRepositoryMock = new();
            _personRepositoryMock = new();
        }

        //TODO unit testing for the COmmand handler.
        [Test]
        public async Task Handle_Should_ReturnFailureResult_UserNotExist()
        {
            // Arrange
            var cmd = new CreateTodoCommand(Guid.Parse("hyunbin7303"), null, "Valid");
            var handler = new CreateTodoCommandHandler(_todoRepositoryMock.Object, _personRepositoryMock.Object, _eventStore);

            //_todoRepositoryMock.Setup(x => x.Find(x=> x.))

            // Act
            //var result = await handler.Handle(cmd, default);

            // Asset
            //result.Should().BeNull(); 
        }
    }
}
