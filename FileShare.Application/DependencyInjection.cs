using FileShare.Application.Auth;

using Microsoft.Extensions.DependencyInjection;

namespace FileShare.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
