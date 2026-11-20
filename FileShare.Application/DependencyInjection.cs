using Microsoft.Extensions.DependencyInjection;

namespace FileShare.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
