﻿using HP.Core.Models;
using HP.Domain.Exceptions;
using MongoDB.Driver;
using System.Data;
using System.Text.RegularExpressions;
using static HP.Domain.TodoDomainEvents;

namespace HP.Domain
{
    public class Todo : AggregateRoot
    {
        public Todo() { InitSetup(); }
        protected Todo(Person person, string title, string description, TodoType todoType, string[] tag) : base()
        {
            InitSetup();
            PersonId = person.Id;
            Title = title;
            Description = description;
            Tag = tag;
            AddDomainEvent(new TodoCreated { TodoId = Id, PersonId = PersonId, TodoTitle = title, TodoDesc = Description, TodoType = todoType.Name });
        }
        private void InitSetup()
        {
            IsActive = true;
            IsDone = false;
            SubTodos = new HashSet<TodoItem>();
            Updated = DateTime.UtcNow;
            CountTodoItem = 0;
        }
        public Guid PersonId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ProjectId { get; private set; }
        public TodoType TodoType { get; private set; }
        public string[] Tag { get; private set; }
        public bool IsDone { get; private set; }
        public bool IsActive { get; private set; }
        public double Score { get; private set; }
        public int CountTodoItem { get; private set; }
        public ICollection<TodoItem> SubTodos { get; private set; }
        public TodoStatus Status { get; private set; }
        public string StatusDesc { get; private set; }
        public DateTime? TargetStartDate { get; private set; }
        public DateTime? TargetEndDate { get; private set; }
        public DateTime? Updated { get; private set; }
        public DateTime? Completed { get; private set; }
        public static Todo Create(Person person, string title, string description, TodoType type, string[] tags)
        {
            if (person is null)
                throw new ArgumentNullException(nameof(person));

            if (string.IsNullOrWhiteSpace(title))
                throw new TodoDomainException("TodoTitle cannot be empty.");

            return new(person, title, description, type, tags);
        }
        public void Update(string title, string type, string desc, string[] Tags, DateTime? targetStartDate = null, DateTime? targetEndDate = null)
        {
            this.Title = title;
            this.Description = desc;
            this.TodoType = TodoType.FromName(type);
            this.TargetStartDate = targetStartDate ?? null;
            this.TargetEndDate = targetEndDate ?? null;
            this.Tag = Tags;
            this.Updated = DateTime.Now;
            this.AddDomainEvent(new TodoUpdated { TodoId = Id, TodoTitle = Title, TodoDesc = Description, TodoType = TodoType.Name });
        }
        public TodoItem AddTodoItem(string title, string type, string desc, DateTime? targetStartDate, DateTime? targetEndDate)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));

            TodoItem todoItem = new TodoItem(title, desc, type, targetStartDate, targetEndDate);
            SubTodos.Add(todoItem);
            CountTodoItem++;
            this.AddDomainEvent(new TodoItemCreated
            {
                TodoItemId = todoItem.Id,
                TodoId = Id,
                TodoTitle = title,
                TodoDesc = desc,
                TodoType = type,
                TargetStartDate = targetStartDate.HasValue ? targetStartDate.Value : null,
                TargetEndDate = targetEndDate.HasValue ? targetEndDate.Value : null
            });
            return todoItem;
        }
        public void DeleteTodoItem(Guid todoItemId)
        {
            var todoItem = SubTodos.FirstOrDefault(x => x.Id == todoItemId);
            if (todoItem == null)
                throw new Exception($"[Domain Excecption]Not Found TodoItem : {todoItemId}");

            SubTodos.Remove(todoItem);
            this.AddDomainEvent(new TodoItemRemoved { TodoItemId = todoItemId });
        }
        public void UpdateTodoItem(Guid todoItemId, string newTitle, string newDesc, string newType)
        {
            var todoItem = SubTodos.FirstOrDefault(x => x.Id == todoItemId);
            if (todoItem == null)
                throw new Exception($"[Domain Excecption]Not Found TodoItem : {todoItemId}");

            todoItem.Update(newTitle, newType, newDesc);
            this.AddDomainEvent(new TodoItemUpdated { TodoItemId = todoItemId });
        }
        public void ActivateTodo()
        {
            this.IsActive = true;
            this.AddDomainEvent(new TodoActivated { TodoId = Id });
        }
        public void DeactivateTodo()
        {
            this.IsActive = false;
            this.AddDomainEvent(new TodoDeactivated { TodoId = Id });
        }
        public void Remove()
        {
            this.IsActive = false;
            this.AddDomainEvent(new TodoRemoved { TodoId = Id });
        }
        public override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case TodoCreated todoCreated:
                    Apply(todoCreated);
                    break;

                case TodoUpdated todoUpdated:
                    Apply(todoUpdated);
                    break;

                case TodoRemoved todoRemoved:
                    Apply(todoRemoved);
                    break;

                case TodoCompleted todoCompleted:
                    Apply(todoCompleted);
                    break;

                case TodoActivated todoActivated:
                    Apply(todoActivated);
                    break;

                case TodoDeactivated todoDeactivated:
                    Apply(todoDeactivated);
                    break;


            }
        }
        private void Apply(TodoCreated @event)
        {
            Id = @event.TodoId;
            PersonId = @event.PersonId;
            Title = @event.TodoTitle;
            Description = @event.TodoDesc;
            Updated = @event.OccuredOn;
            TodoType = TodoType.FromName(@event.TodoType);
        }
        private void Apply(TodoUpdated @event)
        {
            Id = @event.TodoId;
            Title = @event.TodoTitle;
            Description = @event.TodoDesc;
            TodoType = TodoType.FromName(@event.TodoType);
        }
        private void Apply(TodoRemoved @event)
        {
            Id = @event.AggregateId;
        }
        private void Apply(TodoCompleted @event)
        {
            Id = @event.AggregateId;
        }
        private void Apply(TodoActivated @event)
        {
            Id = @event.TodoId;
            IsActive = true;
        }
        private void Apply(TodoDeactivated @event)
        {
            Id = @event.TodoId;
            IsActive = false;
        }

        public void SetStatus(TodoStatus status, string? reason = null)
        {
            switch (status.Name)
            {
                case "pending":
                    this.Status = TodoStatus.Pending;
                    this.StatusDesc = $"Todo Id:{Id} of Title: {Title} is completed.";
                    AddDomainEvent(new TodoStatusToPending { TodoId = Id });
                    break;

                case "accept":
                    this.Status = TodoStatus.Accept;
                    this.StatusDesc = $"Todo Id:{Id} of Title: {Title} is accepted.";
                    AddDomainEvent(new TodoStatusToAccepted { TodoId = Id });
                    break;

                case "start":
                    this.Status = TodoStatus.Start;
                    this.StatusDesc = $"Todo Id:{Id}, has been started at {DateTime.Now}";
                    AddDomainEvent(new TodoStarted { AggregateId = Id });
                    break;

                case "complete":
                    this.Status = TodoStatus.Complete;
                    this.StatusDesc = $"Todo Id:{Id} is completed. ";
                    this.IsDone = true;
                    this.Completed = DateTime.Now;
                    AddDomainEvent(new TodoCompleted { AggregateId = Id });
                    break;

                case "stop":
                    this.Status = TodoStatus.Stop;
                    this.StatusDesc = $"Todo Id:{Id}, has been stopped. Reason: {reason}";
                    //AddDomainEvent(new TodoDomainEvents.TodoStopped);
                    break;

                case "tbd":
                    this.Status = TodoStatus.NotDefined;
                    break;
                default:
                    break;
            }
        }
    }
}