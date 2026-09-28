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
    private readonly IServiceRepository _serviceRepository;

    public HallService(IHallRepository hallRepository, IServiceRepository serviceRepository)
    {
        _hallRepository = hallRepository;
        _serviceRepository = serviceRepository;
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


    /// <summary>
    /// Creates a new hall asynchronously.
    /// </summary>
    /// <param name="hallDto">The CreateHallDto containing the hall details.</param>
    /// <returns>A message about the success of the creation operation.</returns>
    /// <exception cref="ConflictException">Thrown when a hall with the same title already exists.</exception>
    public async Task<string> CreateHallAsync(CreateHallDto hallDto)
    {
        var isHallExists = await _hallRepository.IsHallExistsAsync(hallDto.Title);

        if (isHallExists)
        {
            throw new ConflictException("Зал з такою назвою вже існує");
        }

        if (hallDto.ServiceIds.Count > 0)
        {
            var existingServiceIds = await _serviceRepository
                .GetExistingServiceIdsAsync(hallDto.ServiceIds);

            var invalidServiceIds = hallDto.ServiceIds
                .Except(existingServiceIds)
                .ToList();

            if (invalidServiceIds.Count > 0)
            {
                throw new BadRequestException(
                    $"Обрані послуги з ID {string.Join(", ", invalidServiceIds)} не існують");
            }
        }

        var hall = hallDto.ToHall();
        await _hallRepository.CreateHallAsync(hall);

        return $"Зал з ідентифікатором {hall.Id} було успішно створено";
    }
}
