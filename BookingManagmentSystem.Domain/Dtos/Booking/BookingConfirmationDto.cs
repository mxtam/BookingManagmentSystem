using BookingManagmentSystem.Domain.Dtos.Service;

namespace BookingManagmentSystem.Domain.Dtos.Booking;

/// <summary>
/// Represents the DTO for booking confirmation details, including hall information, booking times, costs, and selected services.
/// </summary>
public class BookingConfirmationDto
{
    public int Id { get; set; }
    public int HallId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal HourlyRate { get; set; }
    public decimal RentalCost { get; set; }
    public decimal ServicesCost { get; set; }
    public decimal TotalCost { get; set; }

    public IReadOnlyList<GetServiceToListDto> SelectedServices { get; set; } = [];
}
