namespace Talabat.APIs.Errors
{
    public class ApiExceptionResponse:ApiResponse
    {

        public string? Details { get; set; }
        public ApiExceptionResponse(int statusCode = 500, string? message= null,string? details =null):base(statusCode,message)
        {
            
            this.StatusCode = statusCode;
            this.Messege = message;
            this.Details = details;
        }

    }
}
