using BookingManagmentSystem.DAL.Data;
using BookingManagmentSystem.Domain.Entities;
using BookingManagmentSystem.Domain.Interfaces.Repository;
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
    public async Task<Hall> GetHallById(int id)
    {
        return await _context.Halls
            .AsNoTracking()
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    /// <summary>
    /// Removes a hall from the database.
    /// </summary>
    /// <param name="hall">The hall to remove.</param>
    public async Task RemoveHallAsync(Hall hall)
    {
        _context.Halls.Remove(hall);
        await _context.SaveChangesAsync();
    }
}
