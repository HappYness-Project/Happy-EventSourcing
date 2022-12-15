using FluentAssertions;
using HP.Domain;
using HP.test;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HP.UnitTest.Todos
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
            Person person = new Person("UserId");

            // Act
            var fakeTodo = Todo.Create(person, "fake Todo", "fake Description", TodoType.Others, faketags);

            //Assert
            fakeTodo.DomainEvents.Count.Should().Be(1);
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
        [Test]
        public void AddTodoTiem_Return_1()
        {
            // Arrange
            var todo = TodoFactory.Create();
            string Title = "Sub Todo Item #1";
            string Type = "Study";
            string Desc = "Description todo Item #1";

            // Act
            var todoItem = todo.AddTodoItem(Title, Type, Desc);

            //Assert
            todo.SubTodos.Should().HaveCount(1);
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

    }
}
