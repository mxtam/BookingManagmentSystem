namespace BookingManagmentSystem.Domain.Entities;

public class Booking
{
    public int Id { get; set; }

    public int HallId { get; set; }
    public Hall Hall { get; set; } = null!;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public decimal HourlyRate { get; set; }
    public decimal RentalCost { get; set; }
    public decimal ServicesCost { get; set; }
    public decimal TotalCost => RentalCost + ServicesCost;

    public ICollection<BookingServices> SelectedServices { get; set; } = new List<BookingServices>();
}
