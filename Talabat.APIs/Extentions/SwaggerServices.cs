using Microsoft.AspNetCore.Builder;

namespace Talabat.APIs.Extentions
{
    public  static class SwaggerServices
    {

        public static IServiceCollection AddSwaggerServices (this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }


        public static WebApplication AddSwaggerMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }

    }
}
