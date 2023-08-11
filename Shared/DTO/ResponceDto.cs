namespace Shared.DTO
{
    public class ResponceDto<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
        public T Data { get; set; }
    }

}
