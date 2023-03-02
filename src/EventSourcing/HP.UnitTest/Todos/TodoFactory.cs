using HP.Domain;
using System;

namespace HP.test
{
    public class TodoFactory
    {
        public static Todo Create()
        {
            return Todo.Create(new Person("UserId"), "Title", "Desc", TodoType.Others, null);
        }
        public static Todo Create(string userId, string title, string type, string desc, bool defaultTag = true)
        {
            string[] tags = { "Study", "Exercise", "Chore" };
            Person person = Person.Create(userId);
            TodoType Type = TodoType.FromName(type);
            return Todo.Create(person, title, desc, Type, defaultTag ? tags : null);
        }
        public static TodoItem CreateTodoItem(string title)
        {
            string Desc = "Description for " + title;
            DateTime? TargetStartDate = DateTime.UtcNow.AddDays(new Random().Next(90));
            TodoItem todoItem = new TodoItem(title, TodoType.Study.ToString(), Desc, TargetStartDate, null);
            return todoItem;
        }
    }
}
