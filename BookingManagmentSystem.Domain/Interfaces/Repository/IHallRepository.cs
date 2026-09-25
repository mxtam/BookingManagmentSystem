using BookingManagmentSystem.Domain.Entities;

namespace BookingManagmentSystem.Domain.Interfaces.Repository;

public interface IHallRepository
{
    /// <summary>
    /// Gets the list of halls asynchronously.
    /// </summary>
    /// <returns>The list of halls with their associated services.</returns>
    Task<IReadOnlyList<Hall>> GetHallsListAsync();

    /// <summary>
    /// Gets a hall by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the hall to retrieve.</param>
    /// <returns>The hall with the specified ID.</returns>
    Task<Hall> GetHallById(int id);

    /// <summary>
    /// Removes a hall asynchronously.
    /// </summary>
    /// <param name="hall">The hall to remove.</param>
    Task RemoveHallAsync(Hall hall);
}
