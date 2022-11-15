namespace HP.Domain.Exceptions
{
    public class PersonDomainException : Exception
    {
        public Person Person { get; }
        public PersonDomainException(string s, Person person) : base(s){ Person = person; }
        public PersonDomainException(string message) : base(message){ }
        public PersonDomainException(string message, Exception innerException): base(message, innerException){ }
    }
}

