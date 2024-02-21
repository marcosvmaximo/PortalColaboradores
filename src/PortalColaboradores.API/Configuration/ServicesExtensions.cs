using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalColaboradores.API.Configuration.Auth;
using PortalColaboradores.Business;
using PortalColaboradores.Business.Interfaces;
using PortalColaboradores.Core.NotificationPattern;
using PortalColaboradores.Infra.Data;
using PortalColaboradores.Infra.ExternalService.Interfaces;
using PortalColaboradores.Infra.ExternalService.Rest;
using PortalColaboradores.Infra.Repositories;

namespace PortalColaboradores.API.Configuration;

public static class ServicesExtensions
{
    public static IServiceCollection AddServicesExtensions(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        // Identity
        services.AddIdentityExtensions(configuration);
        
        // IOC
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<INotifyHandler, NotifyHandler>();

        services.AddTransient<IColaboradorRepository, ColaboradorRepository>();
        services.AddTransient<IColaboradorService, ColaboradorService>();
        services.AddTransient<IViaCepExternalService, ViaCepExternalService>();
        
        // CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        
        // Model state
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        
        return services;
    }

}