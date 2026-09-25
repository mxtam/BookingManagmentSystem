using BookingManagmentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingManagmentSystem.DAL.Data.Configurations;

internal class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Services");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.Price)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.HasMany(s => s.HallServices)
            .WithOne(hs => hs.Service)
            .HasForeignKey(hs => hs.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
