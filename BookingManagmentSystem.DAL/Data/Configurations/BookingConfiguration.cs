using BookingManagmentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingManagmentSystem.DAL.Data.Configurations;

internal class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings", table =>
        {
            table.HasCheckConstraint("CK_Bookings_TimeRange", "[EndTime] > [StartTime]");
            table.HasCheckConstraint("CK_Bookings_HourlyRate", "[HourlyRate] >= 0");
            table.HasCheckConstraint("CK_Bookings_RentalCost", "[RentalCost] >= 0");
            table.HasCheckConstraint("CK_Bookings_ServicesCost", "[ServicesCost] >= 0");
        });

        builder.HasKey(b => b.Id);
        builder.HasIndex(b => new { b.HallId, b.StartTime, b.EndTime });

        builder.Property(b => b.HourlyRate).HasColumnType("decimal(10,2)");
        builder.Property(b => b.RentalCost).HasColumnType("decimal(10,2)");
        builder.Property(b => b.ServicesCost).HasColumnType("decimal(10,2)");
        builder.Ignore(b => b.TotalCost);

        builder.HasOne(b => b.Hall)
            .WithMany()
            .HasForeignKey(b => b.HallId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(b => b.SelectedServices)
            .WithOne(bs => bs.Booking)
            .HasForeignKey(bs => bs.BookingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
