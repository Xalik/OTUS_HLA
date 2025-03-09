using Microsoft.EntityFrameworkCore;
using Npgsql;
using OtusSocNet.DAL;
using OtusSocNet.DAL.Repositories;
using OtusSocNet.DAL.Repositories.Interfaces;
using OtusSocNet.Models.Settings;
using OtusSocNet.Services;
using OtusSocNet.Services.Interfaces;

namespace OtusSocNet.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServiceSettings>(configuration.GetSection(nameof(ServiceSettings)));
        
        return services.AddScoped<IUserService, UserService>();
    }
    
    public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new ArgumentNullException("DefaultConnection connection string is null");

        services.AddScoped<IConnectionFactory, ConnectionFactory>(_ => new ConnectionFactory(connectionString));

        services.AddDbContext<OtusSocNetDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}