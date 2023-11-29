using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;
using Talabat.Service;

namespace Talabat.APIs.Extentions
{
    public static class IdentityServices
    {
        public static IServiceCollection AddIdentityServices (this IServiceCollection services)
        {

           

            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            return services;
        }
    }
}
