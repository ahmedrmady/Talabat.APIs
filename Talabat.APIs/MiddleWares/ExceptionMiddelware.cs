using System.Text.Json;
using System.Text.Json.Serialization;
using Talabat.APIs.Errors;

namespace Talabat.APIs.MiddleWars
{
    public class ExceptionMiddelware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddelware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddelware(RequestDelegate next , ILogger<ExceptionMiddelware> logger,IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        } 

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await next.Invoke(context);
            }
            catch(Exception ex)
            {

                logger.LogError(ex, ex.Message);

                context.Response.ContentType = "applaction/json";
                context.Response.StatusCode = 500;

                var ResponseExcption = env.IsDevelopment() ?
                    new ApiExceptionResponse(500, ex.Message, ex.StackTrace)
                   : new ApiExceptionResponse(500);

                var JsonOptions = new JsonSerializerOptions()
                {
                  PropertyNamingPolicy= JsonNamingPolicy.CamelCase
                };

                var Response = JsonSerializer.Serialize(ResponseExcption,JsonOptions);
                await context.Response.WriteAsync(Response);
                    

            }

        }

    }
}
