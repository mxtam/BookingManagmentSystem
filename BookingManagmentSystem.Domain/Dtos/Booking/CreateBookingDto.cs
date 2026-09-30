using System.ComponentModel.DataAnnotations;

namespace BookingManagmentSystem.Domain.Dtos.Booking;

/// <summary>
/// Represents the DTO for creating a new booking, including hall ID, start time, duration, and selected service IDs.
/// </summary>
public class CreateBookingDto
{
    [Range(1, int.MaxValue, ErrorMessage = "ID залу повинен бути більшим за нуль")]
    public int HallId { get; set; }

    public DateTime StartTime { get; set; }

    [Range(1, 1020, ErrorMessage = "Тривалість бронювання повинна бути від 1 до 1020 хвилин")]
    public int DurationMinutes { get; set; }

    [Required(ErrorMessage = "Список послуг не може бути null")]
    public HashSet<int> ServiceIds { get; set; } = [];
}
