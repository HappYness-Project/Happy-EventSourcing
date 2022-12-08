namespace HP.Core.Commands
{
    public class CommandResult
    {
        public bool IsSuccess { get; set; }
        public string EntityId { get; set; }
        public string Message { get; set; }
        public CommandResult() { }
        public CommandResult(bool isSuccess, string msg = "", string entityId = "")
        {
            IsSuccess = isSuccess;
            EntityId = entityId;
            Message = msg;
        }
    }

}
