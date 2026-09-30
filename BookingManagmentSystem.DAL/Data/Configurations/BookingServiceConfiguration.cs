using BookingManagmentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingManagmentSystem.DAL.Data.Configurations;

internal class BookingServiceConfiguration : IEntityTypeConfiguration<BookingServices>
{
    public void Configure(EntityTypeBuilder<BookingServices> builder)
    {
        builder.ToTable("BookingServices", table =>
            table.HasCheckConstraint("CK_BookingServices_Price", "[Price] >= 0"));

        builder.HasKey(bs => new { bs.BookingId, bs.ServiceId });

        builder.Property(bs => bs.Title)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(bs => bs.Price).HasColumnType("decimal(10,2)");

        builder.HasOne(bs => bs.Service)
            .WithMany()
            .HasForeignKey(bs => bs.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
