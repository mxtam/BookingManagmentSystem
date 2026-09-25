using BookingManagmentSystem.Domain.Dtos.Hall;

namespace BookingManagmentSystem.Domain.Interfaces.Services;

public interface IHallService
{
    /// <summary>
    /// Gets the list of halls asynchronously.
    /// </summary>
    /// <returns>The list of halls with their available services.</returns>
    Task<IReadOnlyList<GetHallToListDto>> GetHallsListAsync();
}
