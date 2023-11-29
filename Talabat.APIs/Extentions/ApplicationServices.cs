using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Helpers;
using Talabat.Core.Repository.Contract;
using Talabat.Repositry.Data;
using Talabat.Repositry;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Errors;
using Talabat.Core;
using Talabat.Core.Services.Contract;
using Talabat.Service;

namespace Talabat.APIs.Extentions
{
    public  static class ApplicationServices
    {

        public static IServiceCollection AddApplicationServices (this IServiceCollection services)
        {
           

           //services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));

           services.AddAutoMapper(typeof(MappingProfiles));
            //webApplicationbuilder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            //add basktRepo
            services.AddScoped(typeof(IBasketRespository),typeof(BasketRepository));

            //add unit of work service
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddScoped(typeof(IProductService),typeof(ProductService));
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(IOrderService), typeof(OrderService));

            // handel validations errors
           services.Configure<ApiBehaviorOptions>(options =>

                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Where(P => P.Value.Errors.Count > 0)
                                                     .SelectMany(P => P.Value.Errors)
                                                     .Select(E => E.ErrorMessage)
                                                     .ToArray();

                    var validationError = new ApiValidationsErrorResponse()
                    {
                        Errors = errors 
                    };

                    return new BadRequestObjectResult(validationError);

                }

                );






            return services;
        }


    }
}
