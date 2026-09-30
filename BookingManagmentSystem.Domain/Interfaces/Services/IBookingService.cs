using BookingManagmentSystem.Domain.Dtos.Booking;
using BookingManagmentSystem.Domain.Dtos.Hall;

namespace BookingManagmentSystem.Domain.Interfaces.Services;

public interface IBookingService
{
    /// <summary>
    /// Gets a list of available halls based on the provided query parameters.
    /// </summary>
    /// <param name="query">The query parameters for filtering available halls.</param>
    /// <returns>A list of available halls.</returns>
    Task<IReadOnlyList<GetHallToListDto>> GetAvailableHallsAsync(AvailableHallsQueryDto query);

    /// <summary>
    /// Creates a new booking based on the provided booking details.
    /// </summary>
    /// <param name="bookingDto">The booking details.</param>
    /// <returns>The confirmation details for the created booking.</returns>
    Task<BookingConfirmationDto> CreateBookingAsync(CreateBookingDto bookingDto);
}
