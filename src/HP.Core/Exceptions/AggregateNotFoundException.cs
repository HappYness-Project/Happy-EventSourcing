namespace HP.Core.Exceptions;
public class AggregateNotFoundException : Exception
{
    public AggregateNotFoundException(string msg) : base(msg){}
}
