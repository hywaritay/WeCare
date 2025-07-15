namespace WeCare.Domain.Utils
{
    public class ErrorHttp
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? Details { get; set; }

        public ErrorHttp(int statusCode, string message, string? details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
    }
}
