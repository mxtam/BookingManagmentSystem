namespace BookingManagmentSystem.Domain.Dtos.Service;

/// <summary>
/// Represents a DTO for a service, including its ID, title, and price.
/// </summary>
public class GetServiceToListDto
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public decimal Price { get; set; }
}
