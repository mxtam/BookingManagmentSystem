namespace BookingManagmentSystem.Domain.Entities;

public class Service
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public decimal Price { get; set; }

    public IEnumerable<HallService> HallServices { get; set; } = new List<HallService>();
}
