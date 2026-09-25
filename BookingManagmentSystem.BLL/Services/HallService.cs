using BookingManagmentSystem.BLL.Mappers;
using BookingManagmentSystem.Domain.Dtos.Hall;
using BookingManagmentSystem.Domain.Entities;
using BookingManagmentSystem.Domain.Exceptions;
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

    /// <summary>
    /// Gets a list of halls asynchronously.
    /// </summary>
    /// <returns>The list of halls with their available services.</returns>
    /// <exception cref="NotFoundException">Thrown when no halls are found.</exception>
    public async Task<IReadOnlyList<GetHallToListDto>> GetHallsListAsync()
    {
        IReadOnlyList<Hall> halls = await _hallRepository.GetHallsListAsync() 
            ?? throw new NotFoundException("Неможливо отримати список залів");

        return halls.Select(h => h.ToGetHallToListDto()).ToList();
    }

    /// <summary>
    /// Removes a hall asynchronously by its ID.
    /// </summary>
    /// <param name="id">The ID of the hall to remove.</param>
    /// <returns>A message representing the asynchronous removal operation.</returns>
    /// <exception cref="NotFoundException">Thrown when the hall is not found.</exception>
    public async Task<string> RemoveHallAsync(int id)
    {
        Hall hall = await _hallRepository.GetHallById(id) 
            ?? throw new NotFoundException("Зал не знайдено");
        

        await _hallRepository.RemoveHallAsync(hall);

        return "Зал було успішно видалено";
    }
}
