
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Everyware.GRDomain.Services.Contracts;
using Everyware.GRInfrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Everyware.GRInfrastructure.Configuration;
using Everyware.GRInfrastructure.Data;
using Microsoft.AspNetCore.Http;
using Everyware.GRDomain.Services;
using Everyware.GRDomain.Interfaces;


namespace Everyware.GRInfrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Everyware");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddBearerAuthentication(configuration);
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.InjectServices(configuration);
        services.InjectRepositories(configuration);
        return services;
    }
    public static IServiceCollection InjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtKey = configuration.GetSection("Jwt:Key").Value;
        var Audience = configuration.GetSection("Jwt:Audience").Value;
        var Issuer = configuration.GetSection("Jwt:Issuer").Value;
        var Expire = Convert.ToDouble(configuration.GetSection("Jwt:Expire").Value);

        services.AddSingleton<IAuthenticationService>(new AuthenticationService(jwtKey, Expire, Issuer, Audience));

        services.AddScoped<IUnitOfWork, UnitOfWork>();




        return services;
    }


    public static IServiceCollection InjectRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        services.AddScoped<IRepositoryFactory, RepositoryFactory>();
        services.CustomRespositoriesInjection();
        return services;
    }
    public static IServiceCollection CustomRespositoriesInjection(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
        //Custom Repositories injection     
        var repositoryTypes =
            typeof(GenericRepository<>).Assembly
                                .ExportedTypes
                                .Where(x => typeof(IRepository<>).IsAssignableFrom(x) &&
                                            !x.IsInterface &&
                                            !x.IsAbstract).ToList();

        repositoryTypes.ForEach(repositoryType =>
        {
            var contract = repositoryType.GetInterface($"I{repositoryType.Name}");

            if (contract != null)
            {
                services.AddScoped(contract, repositoryType);
            }
        });

        return services;
    }

}
