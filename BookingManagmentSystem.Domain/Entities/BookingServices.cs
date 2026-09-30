namespace BookingManagmentSystem.Domain.Entities;

public class BookingServices
{
    public int BookingId { get; set; }
    public Booking Booking { get; set; } = null!;

    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;

    public required string Title { get; set; }
    public decimal Price { get; set; }
}
