using BookingManagmentSystem.Domain.Dtos.Hall;
using BookingManagmentSystem.Domain.Dtos.Service;
using BookingManagmentSystem.Domain.Entities;

namespace BookingManagmentSystem.BLL.Mappers;

internal static class HallMapper
{
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

    public static GetServiceToListDto ToGetServiceToListDto(this Service service)
    {
        return new GetServiceToListDto
        {
            Id = service.Id,
            Title = service.Title,
            Price = service.Price
        };
    }
}
