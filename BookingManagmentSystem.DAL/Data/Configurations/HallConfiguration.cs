using BookingManagmentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingManagmentSystem.DAL.Data.Configurations;

internal class HallConfiguration : IEntityTypeConfiguration<Hall>
{
    public void Configure(EntityTypeBuilder<Hall> builder)
    {
        builder.ToTable("Halls");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.Title)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(h => h.Capacity)
            .IsRequired();

        builder.Property(h => h.Price)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.HasMany(h => h.HallServices)
            .WithOne(hs => hs.Hall)
            .HasForeignKey(hs => hs.HallId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
