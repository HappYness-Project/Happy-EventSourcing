using EventSourcing;

namespace HP.Domain
{
    public class Todo : AggregateRoot<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}