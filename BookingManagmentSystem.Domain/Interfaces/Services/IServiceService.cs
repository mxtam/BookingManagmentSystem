using BookingManagmentSystem.Domain.Dtos.Service;

namespace BookingManagmentSystem.Domain.Interfaces.Services;

public interface IServiceService
{
    /// <summary>
    /// Gets the list of services asynchronously.
    /// </summary>
    /// <returns>Returns a list of services.</returns>
    Task<IReadOnlyList<GetServiceToListDto>> GetServicesListAsync();
}
