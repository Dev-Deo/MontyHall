using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;
using Services;
using System.Reflection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, ConfigurationManager config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString,
                   builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserManagerService, UserManagerService>();
            services.AddScoped<ILoggedInUserService, LoggedInUserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IGameSetupService, GameSetupService>();
            services.AddScoped<IGameResultService, GameResultService>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddCors(opt =>
            {
                opt.AddPolicy(
                    "CorsPolicy",
                    policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
                    }
                );
            });

            return services;
        }
    }
}
