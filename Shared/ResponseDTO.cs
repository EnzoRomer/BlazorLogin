// BlazorLogin.Shared/ResponseDTO.cs
namespace BlazorLogin.Shared
{
    public class ResponseDTO<T>
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Value { get; set; }
    }
}