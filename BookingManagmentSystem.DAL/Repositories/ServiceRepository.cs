using BookingManagmentSystem.DAL.Data;
using BookingManagmentSystem.Domain.Entities;
using BookingManagmentSystem.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingManagmentSystem.DAL.Repositories;

public class ServiceRepository : IServiceRepository
{
    private  readonly AppDbContext _context;

    public ServiceRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets the list of services asynchronously.
    /// </summary>
    /// <returns>Returns a list of services.</returns>
    public async Task<IReadOnlyList<Service>> GetServicesListAsync()
    {
        return await _context.Services.ToListAsync();
    }

    /// <summary>
    /// Gets the existing service IDs from the provided list of service IDs asynchronously.
    /// </summary>
    /// <param name="serviceIds">The list of service IDs to check.</param>
    /// <returns>A set of existing service IDs.</returns>
    public async Task<HashSet<int>> GetExistingServiceIdsAsync(
    IEnumerable<int> serviceIds)
    {
        return await _context.Services
            .Where(s => serviceIds.Contains(s.Id))
            .Select(s => s.Id)
            .ToHashSetAsync();
    }
}
