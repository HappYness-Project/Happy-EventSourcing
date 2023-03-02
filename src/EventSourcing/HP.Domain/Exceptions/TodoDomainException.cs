
namespace HP.Domain.Exceptions
{
    public class TodoDomainException : Exception
    {
        public Todo Todo { get; }
        public TodoDomainException(string s, Todo todo) : base(s){ Todo = todo; }
        public TodoDomainException(string message) : base(message){ }
        public TodoDomainException(string message, Exception innerException): base(message, innerException){ }
    }
}

