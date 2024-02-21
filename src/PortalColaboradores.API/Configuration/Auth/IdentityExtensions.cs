using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortalColaboradores.Business.Entities;
using PortalColaboradores.Infra.Data;

namespace PortalColaboradores.API.Configuration.Auth;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentityExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ApplicationDbContext>(opt => 
            opt.UseSqlServer(connectionString)
        );

        services.AddIdentity<Usuario, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddErrorDescriber<IdentityMessagesPortuguese>()
            .AddDefaultTokenProviders();

        // JWT
        var configIdentitySection = configuration.GetSection("IdentityConfig");
        services.Configure<IdentityConfig>(configIdentitySection);

        var configIdentity = configIdentitySection.Get<IdentityConfig>();
        var key = Encoding.ASCII.GetBytes(configIdentity!.Secret);

        services.AddAuthentication(cfg =>
        {
            cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configIdentity.ValidoEm,
                ValidIssuer = configIdentity.Emissor
            };
        });
            
        return services;
    }
}