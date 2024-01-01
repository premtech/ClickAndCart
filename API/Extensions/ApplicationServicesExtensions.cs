using Core.Interfaces;
using Infrastructue.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
          
            services.AddScoped<IProductRepository, ProductRepository>();
           
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy => 
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            return services;
        }
    }
}