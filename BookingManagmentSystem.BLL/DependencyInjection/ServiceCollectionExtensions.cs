using BookingManagmentSystem.BLL.Services;
using BookingManagmentSystem.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookingManagmentSystem.BLL.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        services.AddScoped<IHallService, HallService>();

        return services;
    }
}