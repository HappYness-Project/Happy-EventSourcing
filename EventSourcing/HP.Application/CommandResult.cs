namespace HP.Application
{
    public class CommandResult
    {
        public string EntityId { get; set; }
        public string Message { get; set; }
        public CommandResult(string msg = "", string entityId = "")
        {
            EntityId = entityId;
            Message = msg;
        }
    }
}
