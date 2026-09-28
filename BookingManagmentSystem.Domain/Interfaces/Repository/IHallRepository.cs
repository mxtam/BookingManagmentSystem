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
    Task<Hall> GetHallByIdAsync(int id);

    /// <summary>
    /// Removes a hall asynchronously.
    /// </summary>
    /// <param name="hall">The hall to remove.</param>
    Task RemoveHallAsync(Hall hall);

    /// <summary>
    /// Creates a new hall asynchronously.
    /// </summary>
    /// <param name="hall">The hall to create.</param>
    Task CreateHallAsync(Hall hall);

    /// <summary>
    /// Checks if a hall with the specified title exists in the database.
    /// </summary>
    /// <param name="title">The title of the hall to check.</param>
    /// <returns>Return true if the hall exists, false otherwise.</returns>
    Task<bool> IsHallExistsAsync(string title);

    /// <summary>
    /// Updates an existing hall asynchronously.
    /// </summary>
    /// <param name="hall">The hall to update.</param>
    Task UpdateHallAsync(Hall hall);
}
