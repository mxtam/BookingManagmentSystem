using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookingManagmentSystem.DAL.Data;
using BookingManagmentSystem.Domain.Interfaces.Repository;
using BookingManagmentSystem.DAL.Repositories;

namespace BookingManagmentSystem.DAL.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Set ConnectionStrings:DefaultConnection before using the database.");
            }

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IHallRepository, HallRepository>();

        return services;
    }
}