using BookingManagmentSystem.Domain.Dtos.Hall;

namespace BookingManagmentSystem.Domain.Interfaces.Services;

public interface IHallService
{
    /// <summary>
    /// Gets the list of halls asynchronously.
    /// </summary>
    /// <returns>The list of halls with their available services.</returns>
    Task<IReadOnlyList<GetHallToListDto>> GetHallsListAsync();

    /// <summary>
    /// Removes a hall asynchronously by its ID.
    /// </summary>
    /// <param name="id">The ID of the hall to remove.</param>
    /// <returns>A message representing the asynchronous removal operation.</returns>
    Task<string> RemoveHallAsync(int id);
}
