using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.APIs.Helpers;
using Talabat.APIs.MiddleWars;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repository.Contract;
using Talabat.Repositry;
using Talabat.Repositry.Data;
using Talabat.Repositry.Data.Data_Seed;
using Talabat.Repositry.Identity;
using Talabat.Repositry.Identity.DataSeed;

namespace Talabat.APIs
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var webApplicationbuilder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services 
            #region Databases

            webApplicationbuilder.Services.AddDbContext<StoreContext>(Options =>
                        Options.UseSqlServer(webApplicationbuilder.Configuration.GetConnectionString("defualConnection"))
               );

            webApplicationbuilder.Services.AddSingleton<IConnectionMultiplexer>(S =>
            {
                var conection = webApplicationbuilder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(conection);

            });

            webApplicationbuilder.Services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(webApplicationbuilder.Configuration.GetConnectionString("IdentityConnection"));  

            });



            #endregion

            //add identity Services to container
          webApplicationbuilder.Services.AddIdentity<AppUser, IdentityRole>()
              .AddEntityFrameworkStores<IdentityDbContext>();

            webApplicationbuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationbuilder.Services.AddSwaggerServices();

            webApplicationbuilder.Services.AddApplicationServices();
            webApplicationbuilder.Services.AddIdentityServices();
            #endregion




             var app = webApplicationbuilder.Build();

            #region Update-DataBase and data seding 

            using var Scope = app.Services.CreateScope();

            var Services = Scope.ServiceProvider;

            var _dbContext = Services.GetRequiredService<StoreContext>();
            var _identitydbContext = Services.GetRequiredService<IdentityDbContext>();
            var _userManager = Services.GetRequiredService<UserManager<AppUser>>();

            var _loggerFactory = Services.GetRequiredService<ILoggerFactory>();

            try
            {
                //database migrate
                await _dbContext.Database.MigrateAsync();
                await _identitydbContext.Database.MigrateAsync();

                //data seeding
                await DataSeeding.DataSeedAsync(_dbContext, _loggerFactory);
                await IdentityDataSeeding.SeedIdentityDataAsync(_userManager);

            }
            catch (Exception ex)
            {

                var _logger = _loggerFactory.CreateLogger<Program>();
                _logger.LogError(ex.ToString(), "There error Ocuard while update DB");

            }


            #endregion

            // Configure the HTTP request pipeline.
            #region Configure Kestral MiddleWares

            app.UseMiddleware<ExceptionMiddelware>();

            if (app.Environment.IsDevelopment())
            {
                app.AddSwaggerMiddlewares();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}