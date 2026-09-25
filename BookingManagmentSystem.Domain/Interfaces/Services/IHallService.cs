using BookingManagmentSystem.Domain.Dtos.Hall;

namespace BookingManagmentSystem.Domain.Interfaces.Services;

public interface IHallService
{
    Task<IReadOnlyList<GetHallToListDto>> GetHallsListAsync();
}
