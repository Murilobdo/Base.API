using System.Text;
using Base.Core.Interfaces.Cache;
using Base.Infra.Cache;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace Base.API.Extensions;

public static class ProgramExtensions
{
    public static void AddCache(this WebApplicationBuilder builder)
    {
        var redisConfiguration = builder.Configuration.GetConnectionString("Redis");
        var redis = ConnectionMultiplexer.Connect(redisConfiguration);
        builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
        builder.Services.AddSingleton<ICacheService, CacheService>(); 
    }
    
    public static void AddControllers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
    }
    
    public static void AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen();
    }
        
    public static void AddJwtAuth(this WebApplicationBuilder builder)
    {
        string keyAuth  = builder.Configuration["AppSettings:KeyAuth"];
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyAuth))
                };
            });

        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();
    }
}