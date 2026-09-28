using BookingManagmentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingManagmentSystem.DAL.Data.Configurations;

internal class HallServiceConfiguration : IEntityTypeConfiguration<HallServices>
{
    public void Configure(EntityTypeBuilder<HallServices> builder)
    {
        builder.ToTable("HallServices");

        builder.HasKey(hs => new
        { 
            hs.HallId,
            hs.ServiceId
        });

        builder.HasOne(hs => hs.Hall)
            .WithMany(h => h.HallServices)
            .HasForeignKey(hs => hs.HallId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(hs => hs.Service)
            .WithMany(s => s.HallServices)
            .HasForeignKey(hs => hs.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
