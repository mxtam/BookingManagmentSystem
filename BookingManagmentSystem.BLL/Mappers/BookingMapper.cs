using BookingManagmentSystem.Domain.Dtos.Booking;
using BookingManagmentSystem.Domain.Dtos.Service;
using BookingManagmentSystem.Domain.Entities;

namespace BookingManagmentSystem.BLL.Mappers;

internal static class BookingMapper
{
    public static BookingConfirmationDto ToBookingConfirmationDto(this Booking booking)
    {
        return new BookingConfirmationDto
        {
            Id = booking.Id,
            HallId = booking.HallId,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            HourlyRate = booking.HourlyRate,
            RentalCost = booking.RentalCost,
            ServicesCost = booking.ServicesCost,
            TotalCost = booking.TotalCost,
            SelectedServices = booking.SelectedServices.Select(service => new GetServiceToListDto
            {
                Id = service.ServiceId,
                Title = service.Title,
                Price = service.Price
            }).ToList()
        };
    }
}
