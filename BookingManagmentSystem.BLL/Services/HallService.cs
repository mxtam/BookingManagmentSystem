using BookingManagmentSystem.BLL.Mappers;
using BookingManagmentSystem.Domain.Dtos.Hall;
using BookingManagmentSystem.Domain.Entities;
using BookingManagmentSystem.Domain.Interfaces.Repository;
using BookingManagmentSystem.Domain.Interfaces.Services;

namespace BookingManagmentSystem.BLL.Services;

public class HallService : IHallService
{
    private readonly IHallRepository _hallRepository;

    public HallService(IHallRepository hallRepository)
    {
        _hallRepository = hallRepository;
    }

    public async Task<IReadOnlyList<GetHallToListDto>> GetHallsListAsync()
    {
        IReadOnlyList<Hall> halls = await _hallRepository.GetHallsListAsync();

        return halls.Select(h => h.ToGetHallToListDto()).ToList();
    }
}
