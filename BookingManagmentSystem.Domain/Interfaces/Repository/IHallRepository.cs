using BookingManagmentSystem.Domain.Entities;

namespace BookingManagmentSystem.Domain.Interfaces.Repository;

public interface IHallRepository
{
    /// <summary>
    /// Gets the list of halls asynchronously.
    /// </summary>
    /// <returns>The list of halls with their associated services.</returns>
    Task<IReadOnlyList<Hall>> GetHallsListAsync();
}
