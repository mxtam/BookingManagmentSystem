using BookingManagmentSystem.Domain.Entities;

namespace BookingManagmentSystem.Domain.Interfaces.Repository;

public interface IHallRepository
{
    Task<IReadOnlyList<Hall>> GetHallsListAsync();
}
