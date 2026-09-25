namespace BookingManagmentSystem.Domain.Entities;

public class Hall
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public int Capacity { get; set; }

    public decimal Price { get; set; }

    public IEnumerable<HallService> HallServices { get; set; } = new List<HallService>();
}
