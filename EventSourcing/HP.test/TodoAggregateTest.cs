using HP.Domain.Todos;
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
            var expectedResult = 1;

            // Act
            var fakeTodo = new Todo("FakeUserName", "fake Todo", "fake type", faketags);

            //Assert
            Assert.Equals(fakeTodo.DomainEvents.Count, expectedResult);
        }

    }
}
