using System.ComponentModel.DataAnnotations;

namespace BookingManagmentSystem.Domain.Dtos.Booking;

/// <summary>
/// Represents the query parameters for retrieving available halls based on specified criteria.
/// </summary>
public class AvailableHallsQueryDto
{
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Місткість повинна бути більшою за нуль")]
    public int Capacity { get; set; }
}
