/*
    @file ServiceExtention.cs
    @brief ServiceExtension class
*/

using HellGateServer.Infrastructure.Repository;
using HellGateServer.Application.Services;
using HellGateServer.Application.Interfaces;

namespace HellGateServer.Extentions;

public static class ServiceExtension
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<UserRepository>();
        services.AddScoped<UserDeviceRepository>();
        services.AddScoped<TokenService>();
        services.AddScoped<AuthService>();
        services.AddScoped<IAuthRepository, AuthRepository>();
    }
}