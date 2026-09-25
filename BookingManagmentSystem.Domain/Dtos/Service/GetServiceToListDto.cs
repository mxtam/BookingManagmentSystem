namespace BookingManagmentSystem.Domain.Dtos.Service;

public class GetServiceToListDto
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public decimal Price { get; set; }
}
