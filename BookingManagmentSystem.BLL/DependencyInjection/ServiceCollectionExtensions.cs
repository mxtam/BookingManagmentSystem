using Microsoft.Extensions.DependencyInjection;

namespace BookingManagmentSystem.BLL.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        // Register the business services of the new project here.
        return services;
    }
}