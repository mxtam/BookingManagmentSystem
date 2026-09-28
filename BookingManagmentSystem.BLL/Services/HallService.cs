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
        Hall hall = await _hallRepository.GetHallByIdAsync(id) 
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

    /// <summary>
    /// Updates an existing hall asynchronously.
    /// </summary>
    /// <param name="id">The ID of the hall to update.</param>
    /// <param name="hallDto">The DTO containing the updated hall information.</param>
    /// <returns>A message about successful update operation.</returns>
    /// <exception cref="NotFoundException">Thrown when the hall is not found.</exception>
    /// <exception cref="ConflictException">Thrown when a hall with the same title already exists.</exception>
    /// <exception cref="BadRequestException">Thrown when the provided service IDs are invalid.</exception>
    public async Task<string> UpdateHallAsync(int id, UpdateHallDto hallDto)
    {
        var hall = await _hallRepository.GetHallByIdAsync(id)
            ?? throw new NotFoundException("Зал не знайдено");

        if (!string.Equals(
                hall.Title,
                hallDto.Title,
                StringComparison.OrdinalIgnoreCase))
        {
            var isHallExists = await _hallRepository
                .IsHallExistsAsync(hallDto.Title);

            if (isHallExists)
            {
                throw new ConflictException(
                    "Зал з такою назвою вже існує");
            }
        }

        var newServiceIds = hallDto.ServiceIds
            .Distinct()
            .ToHashSet();

        if (newServiceIds.Count > 0)
        {
            var existingServiceIds = await _serviceRepository
                .GetExistingServiceIdsAsync(newServiceIds);

            var invalidServiceIds = newServiceIds
                .Except(existingServiceIds)
                .ToList();

            if (invalidServiceIds.Count > 0)
            {
                throw new BadRequestException(
                    $"Обрані послуги з ID {string.Join(", ", invalidServiceIds)} не існують");
            }
        }

        hall.Title = hallDto.Title;
        hall.Capacity = hallDto.Capacity;
        hall.Price = hallDto.Price;

        var currentServiceIds = hall.HallServices
            .Select(hs => hs.ServiceId)
            .ToHashSet();

        var servicesToRemove = hall.HallServices
            .Where(hs => !newServiceIds.Contains(hs.ServiceId))
            .ToList();

        foreach (var hallService in servicesToRemove)
        {
            hall.HallServices.Remove(hallService);
        }

        var serviceIdsToAdd = newServiceIds
            .Except(currentServiceIds);

        foreach (var serviceId in serviceIdsToAdd)
        {
            hall.HallServices.Add(new HallServices
            {
                HallId = hall.Id,
                ServiceId = serviceId
            });
        }

        await _hallRepository.UpdateHallAsync(hall);

        return $"Зал з ідентифікатором {hall.Id} було успішно оновлено";
    }
}
