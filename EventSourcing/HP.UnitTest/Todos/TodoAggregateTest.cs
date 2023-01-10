using FluentAssertions;
using HP.Domain;
using HP.test;
using NUnit.Framework;
using System;
using System.Linq;
using static HP.Domain.TodoDomainEvents;

namespace HP.UnitTest.Todos
{
    public class TodoAggregateTest
    {
        [Test]
        public void Create_new_Todo_And_raises_new_event()
        {
            //Arrange
            string[] faketags = { "Study", "Kevin", "DDD" };
            Person person = new Person(Guid.NewGuid().ToString());
            string todoTitle = "Fake Todo";
            string todoDesc = "Fake Description";
            var expectedEventType = nameof(TodoCreated);
            // Act
            var fakeTodo = Todo.Create(person, todoTitle, todoDesc, TodoType.Others, faketags);

            //Assert
            fakeTodo.Title.Should().NotBeNull().And.Be(todoTitle);
            fakeTodo.Description.Should().NotBeNull().And.Be(todoDesc);
            fakeTodo.Type.Should().NotBeNull().And.Be(TodoType.Others);
            fakeTodo.UncommittedEvents.Should().NotBeNull().And.HaveCount(1);
            var domainEvent = fakeTodo.UncommittedEvents.First();
            domainEvent.EventType.Should().Be(expectedEventType);
        }

        [Test]
        public void Todo_Is_Activated_And_Raise_Event()
        {
            // Arrange
            var todo = TodoFactory.Create();
            var expectedEventType = nameof(TodoActivated);

            //Act 
            todo.ActivateTodo();

            //Assert
            todo.IsActive.Should().BeTrue();
            todo.UncommittedEvents.Should().NotBeNull().And.HaveCount(2);
            var domainEvent = todo.UncommittedEvents.Last();
            domainEvent.EventType.Should().Be(expectedEventType);
        }

        [Test]
        public void Todo_Is_Deactivated_And_Raise_Event()
        {
            // Arrange
            var todo = TodoFactory.Create();
            var expectedEventType = nameof(TodoDeactivated);

            // Act
            todo.DeactivateTodo();

            //Assert
            todo.IsActive.Should().BeFalse();
            todo.UncommittedEvents.Should().NotBeNull().And.HaveCount(2);
            var domainEvent = todo.UncommittedEvents.Last();
            domainEvent.EventType.Should().Be(expectedEventType);
        }

        [Test]
        public void Todo_Is_Updated_And_Raise_Event()
        {
            // Arrange
            var todo = TodoFactory.Create("Hyunbin7303", type: "",todoTitle:"Testing the new Todo", desc:"Description");

            // Act
            todo.Update("Updated Todo Title", type: "Study", "Description updated", null);

            // Assert.
            //todo.Updated
        }

        [Test]
        public void AddTodoTiem_Count_Should_Be_One()
        {
            // Arrange
            var todo = TodoFactory.Create("", "MainTodo",TodoType.Others.ToString(), "Description");

            // Act
            todo.AddTodoItem("Sub Todo Item #1", TodoType.Study.ToString(), "Description todo Item #1");

            //Assert
            todo.SubTodos.Should().HaveCount(1);
            todo.UncommittedEvents.Should().HaveCount(1);
        }

        [Test]
        public void DeleteTodoItem_Return_0()
        {
            // Arrange
            var todo = TodoFactory.Create();
            string Title = "Sub Todo Item #1";
            string Type = "Study";
            string Desc = "Description todo Item #1";
            var todoItem = todo.AddTodoItem(Title, Type, Desc);

            // Act
            todo.DeleteTodoItem(todoItem.Id);

            //Assert
            todo.SubTodos.Should().HaveCount(0);
        }

        [Test]
        public void Create_new_Todo_ThrowException_When_Person_Is_Null()
        {
            //Arrange
            string[] faketags = { "Study", "Kevin", "DDD" };
            Person person = new Person(Guid.NewGuid().ToString());
            string todoTitle = "Fake Todo";
            string todoDesc = "Fake Description";
            var expectedEventType = nameof(TodoCreated);

            // Act
            Action act = () =>  Todo.Create(null, todoTitle, todoDesc, TodoType.Others, faketags);

            act.Should().Throw<ArgumentNullException>();
        }

    }
}
