namespace HP.Shared
{
    public class ServiceResult<T>
    {
        public T? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Msg { get; set; } = string.Empty;
    }
}
