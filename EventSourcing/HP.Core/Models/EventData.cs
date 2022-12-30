namespace HP.Core.Models
{
    public class EventData
    { 
        protected EventData(string type)
        {
            this.Type = type;
        }
        public Guid Id {get; set; }
        public int Version {get; set;}
        public string Type { get; set; }
    }
}
