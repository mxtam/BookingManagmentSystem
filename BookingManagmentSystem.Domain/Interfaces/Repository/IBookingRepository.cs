using BookingManagmentSystem.Domain.Entities;

namespace BookingManagmentSystem.Domain.Interfaces.Repository;

public interface IBookingRepository
{
    /// <summary>
    /// Returns a list of halls that are available for booking 
    /// within the specified time interval and meet the capacity requirement.
    /// </summary>
    /// <param name="startTime">The start time of the booking interval.</param>
    /// <param name="endTime">The end time of the booking interval.</param>
    /// <param name="capacity">The minimum capacity required for the halls.</param>
    /// <returns>A list of available halls.</returns>
    Task<IReadOnlyList<Hall>> GetAvailableHallsAsync(DateTime startTime, DateTime endTime, int capacity);

    /// <summary>
    /// Checks if there is an overlap in bookings for a specific hall within the given time range.
    /// </summary>
    /// <param name="hallId">The ID of the hall to check.</param>
    /// <param name="startTime">The start time of the booking interval.</param>
    /// <param name="endTime">The end time of the booking interval.</param>
    /// <returns>True if there is an overlap, otherwise false.</returns>
    Task<bool> HasOverlapAsync(int hallId, DateTime startTime, DateTime endTime);

    /// <summary>
    /// Creates a new booking in the system.
    /// </summary>
    /// <param name="booking">The booking to create.</param>
    Task CreateBookingAsync(Booking booking);
}
