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

    /// <summary>
    /// Creates a new hall asynchronously based on the provided CreateHallDto.
    /// </summary>
    /// <param name="hallDto">The DTO containing the hall information.</param>
    /// <returns>A message representing the asynchronous creation operation.</returns>
    Task<string> CreateHallAsync(CreateHallDto hallDto);

    /// <summary>
    /// Updates the hall asynchronously.
    /// </summary>
    /// <param name="id">The ID of the hall to update.</param>
    /// <param name="hallDto">The DTO containing the updated hall information.</param>
    /// <returns>A message about successful update operation.</returns>
    Task<string> UpdateHallAsync(int id, UpdateHallDto hallDto);
}
