namespace HP.Shared
{
    public class Result<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Msg { get; set; } = string.Empty;
    }
}
