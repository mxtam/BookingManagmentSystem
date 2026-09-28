using BookingManagmentSystem.BLL.Mappers;
using BookingManagmentSystem.Domain.Dtos.Service;
using BookingManagmentSystem.Domain.Interfaces.Repository;
using BookingManagmentSystem.Domain.Interfaces.Services;

namespace BookingManagmentSystem.BLL.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceService(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    /// <summary>
    /// Gets the list of services asynchronously.
    /// </summary>
    /// <returns>Returns a list of services.</returns>
    public async Task<IReadOnlyList<GetServiceToListDto>> GetServicesListAsync()
    {
        var services = await _serviceRepository.GetServicesListAsync();

        return services.Select(s => s.ToGetServiceToListDto()).ToList();
    }
}
