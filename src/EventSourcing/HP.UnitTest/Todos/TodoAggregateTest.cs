using FluentAssertions;
using HP.Domain;
using HP.Domain.Exceptions;
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
            string[] faketags = { "Study", "Kevin", "DDD" };
            string userId = "hyunbin7303";
            string email = "hyunbin7303@gmail.com";
            Person person = new Person(email, userId);
            string todoTitle = "Fake Todo";
            string todoDesc = "Fake Description";
            var expectedEventType = nameof(TodoCreated);

            var fakeTodo = Todo.Create(person, todoTitle, todoDesc, TodoType.Others, faketags);

            //Assert
            fakeTodo.Title.Should().NotBeNull().And.Be(todoTitle);
            fakeTodo.Description.Should().NotBeNull().And.Be(todoDesc);
            fakeTodo.TodoType.Should().NotBeNull().And.Be(TodoType.Others);
            fakeTodo.UncommittedEvents.Should().NotBeNull().And.HaveCount(1);

            var domainEvent = fakeTodo.UncommittedEvents.First();
            domainEvent.EventType.Should().Be(expectedEventType);
            domainEvent.AggregateVersion.Should().Be(0);    
        }
        [Test]
        public void Create_New_Todo_ThrowException_TodoTitle_Null()
        {
            string[] faketags = { "Study", "Kevin", "DDD" };
            string userId = "hyunbin7303";
            Person person = new Person(userId, Guid.NewGuid().ToString());
            string todoTitle = string.Empty;
            string todoDesc = "Fake Description";
            var expectedEventType = nameof(TodoCreated);

            // Act
            Action act = () => Todo.Create(person, todoTitle, todoDesc, TodoType.Others, faketags);

            //Assert
            act.Should().Throw<TodoDomainException>("[TodoException]TodoTitle cannot be empty.");
        }
        [Test]
        public void Create_new_Todo_ThrowException_When_Person_Is_Null()
        {
            string[] faketags = { "Study", "Kevin", "DDD" };
            string userId = "hyunbin7303";
            Person person = new Person(userId,Guid.NewGuid().ToString());
            string todoTitle = "Fake Todo";
            string todoDesc = "Fake Description";

            // Act
            Action act = () => Todo.Create(null, todoTitle, todoDesc, TodoType.Others, faketags);

            act.Should().Throw<ArgumentNullException>();
        }
        [Test]
        public void Todo_Activate_Success()
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
        public void Todo_Deactivate_Success()
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
        public void Todo_Update_Success()
        {
            // Arrange
            string todoTitle = "Updated Todo Title";
            string todoType = "Study";
            string todoDesc = "Description updated";
            DateTime targetStartDate = new DateTime(2023, 1, 1);
            var todo = TodoFactory.Create("Hyunbin7303", "Testing the new Todo", "Others", desc:"Description");

            // Act
            todo.Update(todoTitle, todoType, todoDesc, null, targetStartDate: targetStartDate);

            // Assert.
            todo.UncommittedEvents.Count().Should().Be(2);
            todo.Title.Should().Be(todoTitle);
            todo.Description.Should().Be(todoDesc);
            todo.TodoType.Name.Should().Be(todoType);    
        }
        [Test]
        public void Todo_Update_ThrowException_When_TodoTitle_Is_Null()
        {

        }
        [Test]
        public void Create_Todo_And_Add_TodoItem_Success()
        {
            // Arrange
            var todo = TodoFactory.Create("", "MainTodo",TodoType.Others.ToString(), "Description");
            var expectedEventNum = 2; // CreateTodo Event, AddTodoItem Event

            // Act
            todo.AddTodoItem("Sub Todo Item #1", TodoType.Study.ToString(), "Description todo Item #1", null,null);

            //Assert
            todo.SubTodos.Should().HaveCount(1);
            todo.CountTodoItem.Should().Be(1);
            todo.UncommittedEvents.Should().HaveCount(expectedEventNum);
        }
        [Test]
        public void Delete_TodoItem_Success()
        {
            // Arrange
            var todo = TodoFactory.Create();
            int expectedEventCount = 3; 
            string Title = "Sub Todo Item #1";
            string Desc = "Description todo Item #1";
            DateTime? TargetStartDate = new DateTime(2022, 12, 25);
            DateTime? TargetEndDate = new DateTime(2022, 12, 30);
            var todoItem = todo.AddTodoItem(Title, TodoType.Study.ToString(), Desc, TargetStartDate, TargetEndDate);

            // Act
            todo.DeleteTodoItem(todoItem.Id);

            //Assert
            todo.SubTodos.Should().HaveCount(0);
            todo.UncommittedEvents.Should().HaveCount(expectedEventCount);
        }


        // validation Checking Method for SubTodoItem? 
        [Test]
        public void Create_Multiple_SubTodo_Should_Count_Valid()
        {
            var todo = TodoFactory.Create();
            var todoItem = TodoFactory.CreateTodoItem("Sub Todo Item #1");
            todo.SubTodos.Add(todoItem);

            todo.CountTodoItem.Should().Be(1);

            var todoItem2 = TodoFactory.CreateTodoItem("Sub Todo Item #2");
            todo.SubTodos.Add(todoItem2);
            todo.CountTodoItem.Should().Be(2);

            var todoItem3 = TodoFactory.CreateTodoItem("Sub Todo Item #3");
            todo.SubTodos.Add(todoItem3);
            todo.CountTodoItem.Should().Be(3);
            todo.SubTodos.Count().Should().Be(3);
        }

    }
}
