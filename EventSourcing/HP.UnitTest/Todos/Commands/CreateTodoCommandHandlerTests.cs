using HP.Application.Commands;
using HP.Domain;
using HP.test;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;
using HP.Application.DTOs;

namespace HP.UnitTest.Todos.Commands
{
    public class CreateTodoCommandHandlerTests : TestBase
    {
        private readonly Mock<ITodoRepository> _todoRepositoryMock;
        private readonly Mock<IPersonRepository> _personRepositoryMock;

        [Test]
        public async Task Handle_Should_ReturnFailureResult_WhenTitleIsEmpty()
        {
            // Arrange
            var cmd = new CreateTodoCommand("hyunbin7303", null, "Valid");
            var handler = new CreateTodoCommandHandler(_mapper, _todoRepositoryMock.Object, _personRepositoryMock.Object);

            // Act
            TodoDetailsDto result = await handler.Handle(cmd, default);

            // Asset
            result.TodoTitle.Should().BeNull();
        }
    }
}
