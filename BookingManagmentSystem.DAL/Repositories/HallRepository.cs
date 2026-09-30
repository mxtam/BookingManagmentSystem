using BookingManagmentSystem.DAL.Data;
using BookingManagmentSystem.Domain.Entities;
using BookingManagmentSystem.Domain.Exceptions;
using BookingManagmentSystem.Domain.Interfaces.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookingManagmentSystem.DAL.Repositories;

internal class HallRepository : IHallRepository
{
    private readonly AppDbContext _context;

    public HallRepository(AppDbContext context)
    {
        _context = context;
    }    

    /// <summary>
    /// Gets a list of all halls from the database, including their associated services.
    /// </summary>
    /// <returns>The list of halls with their associated services.</returns>
    public async Task<IReadOnlyList<Hall>> GetHallsListAsync()
    {
        return await _context.Halls
            .Include(h => h.HallServices)
                .ThenInclude(hs => hs.Service)
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Gets a hall by its ID from the database.
    /// </summary>
    /// <param name="id">The ID of the hall to retrieve.</param>
    /// <returns>The hall with the specified ID.</returns>
    public async Task<Hall> GetHallByIdAsync(int id)
    {
        return await _context.Halls
            .Include(h => h.HallServices)
                .ThenInclude(hs => hs.Service)
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    /// <summary>
    /// Removes a hall from the database.
    /// </summary>
    /// <param name="hall">The hall to remove.</param>
    public async Task RemoveHallAsync(Hall hall)
    {
        if (await _context.Bookings.AnyAsync(b => b.HallId == hall.Id))
        {
            throw new ConflictException("Зал має існуючі броні на вказаний час.");
        }

        _context.Halls.Remove(hall);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException exception) when (
            exception.InnerException is SqlException { Number: 547 } sqlException &&
            sqlException.Message.Contains("FK_Bookings_Halls_HallId", StringComparison.Ordinal))
        {
            throw new ConflictException("Зал з існуючими бронюваннями не може бути видалений.");
        }
    }

    /// <summary>
    /// Creates a new hall in the database.
    /// </summary>
    /// <param name="hall">The hall to create.</param>
    public async Task CreateHallAsync(Hall hall)
    {
        _context.Halls.Add(hall);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Checks if a hall with the specified title exists in the database.
    /// </summary>
    /// <param name="title">The title of the hall to check.</param>
    /// <returns>Return true if the hall exists, false otherwise.</returns>
    public async Task<bool> IsHallExistsAsync(string title)
    {
        return await _context.Halls.AnyAsync(h => h.Title == title);
    }

    /// <summary>
    /// Updates an existing hall in the database.
    /// </summary>
    /// <param name="hall">The hall to update.</param>
    public async Task UpdateHallAsync(Hall hall)
    {
        _context.Halls.Update(hall);
        await _context.SaveChangesAsync();
    }
}
