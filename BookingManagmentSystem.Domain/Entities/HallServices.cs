namespace BookingManagmentSystem.Domain.Entities;

public class HallServices
{
    public int HallId { get; set; }
    public Hall Hall { get; set; } = null!;

    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;
}
