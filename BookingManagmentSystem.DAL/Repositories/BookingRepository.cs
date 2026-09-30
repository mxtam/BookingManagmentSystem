using BookingManagmentSystem.DAL.Data;
using BookingManagmentSystem.Domain.Entities;
using BookingManagmentSystem.Domain.Exceptions;
using BookingManagmentSystem.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingManagmentSystem.DAL.Repositories;

internal class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets a list of available halls that meet the specified capacity and are not booked during the given time range.
    /// </summary>
    /// <param name="startTime">The start time of the requested time range.</param>
    /// <param name="endTime">The end time of the requested time range.</param>
    /// <param name="capacity">The minimum capacity required for the halls.</param>
    /// <returns>A list of available halls.</returns>
    public async Task<IReadOnlyList<Hall>> GetAvailableHallsAsync(
        DateTime startTime, DateTime endTime, int capacity)
    {
        return await _context.Halls
            .Where(h => h.Capacity >= capacity && !_context.Bookings.Any(b =>
                b.HallId == h.Id && b.StartTime < endTime && startTime < b.EndTime))
            .Include(h => h.HallServices)
                .ThenInclude(hs => hs.Service)
            .AsNoTracking()
            .OrderBy(h => h.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Checks if there is an overlap in bookings for a specific hall within the given time range.
    /// </summary>
    /// <param name="hallId">The ID of the hall to check.</param>
    /// <param name="startTime">The start time of the requested time range.</param>
    /// <param name="endTime">The end time of the requested time range.</param>
    /// <returns>true if there is an overlap; otherwise, false.</returns>
    public Task<bool> HasOverlapAsync(int hallId, DateTime startTime, DateTime endTime)
    {
        return _context.Bookings.AnyAsync(b =>
            b.HallId == hallId && b.StartTime < endTime && startTime < b.EndTime);
    }

    /// <summary>
    /// Creates a new booking for a hall, ensuring that the hall exists and that there are no overlapping bookings for the specified time range.
    /// </summary>
    /// <param name="booking">The booking to create.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown when the specified hall is not found.</exception>
    /// <exception cref="ConflictException">Thrown when there is an overlapping booking for the specified time range.</exception>
    public async Task CreateBookingAsync(Booking booking)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        var hall = await _context.Halls
            .FromSqlInterpolated($"SELECT * FROM [Halls] WITH (UPDLOCK, HOLDLOCK) WHERE [Id] = {booking.HallId}")
            .AsNoTracking()
            .SingleOrDefaultAsync();

        if (hall is null)
        {
            throw new NotFoundException($"Зал з таким ID {booking.HallId} не знайдено.");
        }

        if (await HasOverlapAsync(booking.HallId, booking.StartTime, booking.EndTime))
        {
            throw new ConflictException("Зал вже заброньований на вказаний час.");
        }

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }
}
