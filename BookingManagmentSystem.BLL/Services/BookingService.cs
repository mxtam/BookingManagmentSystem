using BookingManagmentSystem.BLL.Mappers;
using BookingManagmentSystem.Domain.Dtos.Booking;
using BookingManagmentSystem.Domain.Dtos.Hall;
using BookingManagmentSystem.Domain.Entities;
using BookingManagmentSystem.Domain.Exceptions;
using BookingManagmentSystem.Domain.Interfaces.Repository;
using BookingManagmentSystem.Domain.Interfaces.Services;

namespace BookingManagmentSystem.BLL.Services;

public class BookingService : IBookingService
{
    private readonly IHallRepository _hallRepository;
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IHallRepository hallRepository, IBookingRepository bookingRepository)
    {
        _hallRepository = hallRepository;
        _bookingRepository = bookingRepository;
    }

    /// <summary>
    /// Retrieves a list of available halls based on the specified query parameters.
    /// </summary>
    /// <param name="query">The query parameters.</param>
    /// <returns>The list of available halls.</returns>
    /// <exception cref="BadRequestException">Thrown when the query parameters are invalid.</exception>
    public async Task<IReadOnlyList<GetHallToListDto>> GetAvailableHallsAsync(AvailableHallsQueryDto query)
    {
        if (query.Capacity <= 0)
        {
            throw new BadRequestException("Місткість повинна бути більшою за нуль");
        }

        BookingTimeRules.ValidateInterval(query.StartTime, query.EndTime);
        var halls = await _bookingRepository.GetAvailableHallsAsync(
            query.StartTime, query.EndTime, query.Capacity);

        return halls.Select(hall => hall.ToGetHallToListDto()).ToList();
    }

    /// <summary>
    /// Creates a new booking for a hall with selected services.
    /// </summary>
    /// <param name="bookingDto">The booking details.</param>
    /// <returns>The confirmed booking details.</returns>
    /// <exception cref="BadRequestException">Thrown when the booking details are invalid.</exception>
    /// <exception cref="NotFoundException">Thrown when the hall or services are not found.</exception>
    /// <exception cref="ConflictException">Thrown when the hall is already booked for the selected time.</exception>
    public async Task<BookingConfirmationDto> CreateBookingAsync(CreateBookingDto bookingDto)
    {
        if (bookingDto.HallId <= 0)
        {
            throw new BadRequestException("ID залу повинен бути більшим за нуль");
        }

        if (bookingDto.ServiceIds is null || bookingDto.ServiceIds.Any(id => id <= 0))
        {
            throw new BadRequestException("Список послуг не може бути null та повинен містити лише додатні ID");
        }

        var endTime = BookingTimeRules.GetEndTime(bookingDto.StartTime, bookingDto.DurationMinutes);
        var hall = await _hallRepository.GetHallByIdAsync(bookingDto.HallId)
            ?? throw new NotFoundException("Зал не знайдено");

        var availableServiceIds = hall.HallServices.Select(service => service.ServiceId).ToHashSet();
        var invalidServiceIds = bookingDto.ServiceIds.Except(availableServiceIds).OrderBy(id => id).ToList();
        if (invalidServiceIds.Count > 0)
        {
            throw new BadRequestException(
                $"Послуги з ID {string.Join(", ", invalidServiceIds)} недоступні для обраного залу");
        }

        if (await _bookingRepository.HasOverlapAsync(hall.Id, bookingDto.StartTime, endTime))
        {
            throw new ConflictException("Зал уже заброньовано на обраний час");
        }

        var selectedServices = hall.HallServices
            .Where(service => bookingDto.ServiceIds.Contains(service.ServiceId))
            .OrderBy(service => service.ServiceId)
            .Select(service => new BookingServices
            {
                ServiceId = service.ServiceId,
                Title = service.Service.Title,
                Price = service.Service.Price
            })
            .ToList();

        var booking = new Booking
        {
            HallId = hall.Id,
            StartTime = bookingDto.StartTime,
            EndTime = endTime,
            HourlyRate = hall.Price,
            RentalCost = BookingPriceCalculator.CalculateRentalCost(hall.Price, bookingDto.StartTime, endTime),
            ServicesCost = selectedServices.Sum(service => service.Price),
            SelectedServices = selectedServices
        };

        await _bookingRepository.CreateBookingAsync(booking);
        return booking.ToBookingConfirmationDto();
    }
}
