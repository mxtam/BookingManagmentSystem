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

    public async Task<IReadOnlyList<Hall>> GetHallsListAsync()
    {
        return await _context.Halls
            .Include(h => h.HallServices)
                .ThenInclude(hs => hs.Service)
            .AsNoTracking()
            .ToListAsync();
    }
}
