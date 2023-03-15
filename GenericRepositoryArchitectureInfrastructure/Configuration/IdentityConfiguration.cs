using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Everyware.GRInfrastructure.Configuration;

public static class IdentityConfiguration
{
    public static IServiceCollection AddBearerAuthentication(this IServiceCollection services, IConfiguration configurationManager)
    {
        var key = configurationManager["Jwt:Key"];
        var ValidIssuer = configurationManager["Jwt:Issuer"];
        var ValidAudience = configurationManager["Jwt:Audience"];

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = ValidIssuer,
                    ValidAudience = ValidAudience

                };
            });

        return services;
    }
}