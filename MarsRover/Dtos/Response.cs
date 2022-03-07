namespace MarsRover.Dtos
{
    public class Response<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public T ResponseItem { get; set; }   
    }
}
