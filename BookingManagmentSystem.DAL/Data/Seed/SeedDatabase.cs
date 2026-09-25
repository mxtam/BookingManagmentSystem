using BookingManagmentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingManagmentSystem.DAL.Data.Seed;

public static class SeedDatabase
{
    public static async Task ClearAsync(AppDbContext context)
    {
        await context.HallServices.ExecuteDeleteAsync();
        await context.Halls.ExecuteDeleteAsync();
        await context.Services.ExecuteDeleteAsync();
    }

    public static async Task SeedAsync(AppDbContext context)
    {
        Service projector = new Service 
        { 
            Title = "Проєктор",
            Price = 500.00m
        };

        Service wifi = new Service 
        { 
            Title = "Wi-Fi",
            Price = 300.00m
        };

        Service sound = new Service
        {
            Title = "Звук",
            Price = 700.00m
        };

        Hall hallA = new Hall
        {
            Title = "Зал A",
            Capacity = 50,
            Price = 2000.00m
        };

        Hall hallB = new Hall
        {
            Title = "Зал B",
            Capacity = 100,
            Price = 3500.00m
        };

        Hall hallC = new Hall
        {
            Title = "Зал C",
            Capacity = 30,
            Price = 1500.00m
        };

        var hallServices = new[] 
        { 
            new HallService
            {
                Hall = hallA,
                Service = projector
            },
            new HallService
            {
                Hall = hallA,
                Service = wifi
            },
            new HallService
            {
                Hall = hallB,
                Service = sound
            },
            new HallService
            {
                Hall = hallB,
                Service = projector
            },
            new HallService
            {
                Hall = hallB,
                Service = wifi
            },
            new HallService
            {
                Hall = hallC,
                Service = wifi
            },
        };

        await context.HallServices.AddRangeAsync(hallServices);

        await context.SaveChangesAsync();
    }
}
