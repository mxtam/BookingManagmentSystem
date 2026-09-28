using BookingManagmentSystem.Domain.Entities;

namespace BookingManagmentSystem.Domain.Interfaces.Repository;

public interface IServiceRepository
{
    /// <summary>
    /// Gets the list of services asynchronously.
    /// </summary>
    /// <returns>Returns a list of services.</returns>
    Task<IReadOnlyList<Service>> GetServicesListAsync();

    /// <summary>
    /// Gets the existing service IDs from the provided list of service IDs asynchronously.
    /// </summary>
    /// <param name="serviceIds">The list of service IDs to check.</param>
    /// <returns>A set of existing service IDs.</returns>
    Task<HashSet<int>> GetExistingServiceIdsAsync(IEnumerable<int> serviceIds);
}
