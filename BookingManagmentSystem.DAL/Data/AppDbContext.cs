using BookingManagmentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingManagmentSystem.DAL.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Hall> Halls => Set<Hall>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<HallService> HallServices => Set<HallService>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}