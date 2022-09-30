using FluentAssertions;
using HP.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HP.test
{
    public class TodoAggregateTest
    {
        public TodoAggregateTest()
        {

        }
        [Test]
        public void Create_new_Todo_raises_new_event()
        {
            //Arrange
            string[] faketags = { "Study", "Kevin", "DDD" };
            List<Todo> todoList = new List<Todo>();
            Address addr = new Address("Canada", "Waterloo", "ON", "n2l-4m2");
            var expectedResult = 1;
            Person person= new Person("Kevin", "Park", addr, null);

            // Act
            var fakeTodo = Todo.Create(person, "fake Todo", "fake Description", TodoType.Others, faketags);

            //Assert
            Assert.Equals(fakeTodo.DomainEvents.Count, expectedResult);
        }

        [Test]
        public void ActivateTodo_Todo_Activated_True()
        {
            // Arrange
            var todo = TodoFactory.Create();

            //Act 
            todo.ActivateTodo(todo.Id);

            //Assert
            todo.IsActive.Should().BeTrue();
        }

        [Test]
        public void DeactivateTodo_Todo_Is_Deactivated()
        {
            // Arrange
            var todo = TodoFactory.Create();

            // Act
            todo.DeactivateTodo(todo.Id);

            //Assert
            todo.IsActive.Should().BeFalse();
        }
    }
}
