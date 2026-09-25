using BookingManagmentSystem.Domain.Dtos.Service;

namespace BookingManagmentSystem.Domain.Dtos.Hall;

/// <summary>
/// Represents a DTO for a hall, including its ID, title, capacity, price, and available services.
/// </summary>
public class GetHallToListDto
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public int Capacity { get; set; }

    public decimal Price { get; set; }

    public IEnumerable<GetServiceToListDto> AvailableServices { get; set; } = new List<GetServiceToListDto>();
}
