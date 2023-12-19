using HP.Core.Models;

namespace HP.Core.Test
{
    public class DummyAggregate : AggregateRoot
    {
        public override void When(IDomainEvent @event)
        {
            throw new NotImplementedException();
        }
        //https://github.com/oskardudycz/EventSourcing.NetCore/blob/main/Core.Tests/AggregateWithWhenTests.cs
    }
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}