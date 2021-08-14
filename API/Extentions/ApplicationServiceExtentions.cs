using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extentions
{
    public static class ApplicationServiceExtentions
    {
      public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ICustomerService, CustomerService>();
             services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddIdentity<ApplicationUser, IdentityRole>()  
                .AddEntityFrameworkStores<ApplicationDbContext>()  ;
                //.AddDefaultTokenProviders();  

            return services;
        }
    }
}