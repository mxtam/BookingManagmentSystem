using BookingManagmentSystem.Domain.Dtos.Hall;
using BookingManagmentSystem.Domain.Dtos.Service;
using BookingManagmentSystem.Domain.Entities;

namespace BookingManagmentSystem.BLL.Mappers;

internal static class HallMapper
{
    /// <summary>
    /// Maps a Hall entity to a GetHallToListDto.
    /// </summary>
    /// <param name="hall">The Hall entity to map.</param>
    /// <returns>The GetHallToListDto.</returns>
    public static GetHallToListDto ToGetHallToListDto(this Hall hall)
    {
        return new GetHallToListDto
        {
            Id = hall.Id,
            Title = hall.Title,
            Capacity = hall.Capacity,
            Price = hall.Price,
            AvailableServices = hall.HallServices
                .Select(hs => hs.Service.ToGetServiceToListDto())
        };
    }
    
    /// <summary>
    /// Maps a Service entity to a GetServiceToListDto.
    /// </summary>
    /// <param name="service">The Service entity to map.</param>
    /// <returns>The GetServiceToListDto.</returns>
    public static GetServiceToListDto ToGetServiceToListDto(this Service service)
    {
        return new GetServiceToListDto
        {
            Id = service.Id,
            Title = service.Title,
            Price = service.Price
        };
    }

    /// <summary>
    /// Maps a CreateHallDto to a Hall entity.
    /// </summary>
    /// <param name="hallDto">The CreateHallDto to map.</param>
    /// <returns>The Hall entity.</returns>
    public static Hall ToHall(this CreateHallDto hallDto)
    {
        return new Hall
        {
            Title = hallDto.Title,
            Capacity = hallDto.Capacity,
            Price = hallDto.Price,
            HallServices = hallDto.ServiceIds.Select(serviceId => new HallServices
            {
                ServiceId = serviceId
            }).ToList()
        };
    }
}
