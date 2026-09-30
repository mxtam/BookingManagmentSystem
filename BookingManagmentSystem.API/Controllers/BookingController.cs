using BookingManagmentSystem.Domain.Dtos.Booking;
using BookingManagmentSystem.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagmentSystem.API.Controllers;

[Route("api/bookings")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    /// <summary>
    /// Creates a new booking based on the provided booking details.
    /// </summary>
    /// <param name="bookingDto">The booking details.</param>
    /// <returns>The confirmed booking details.</returns>
    [HttpPost]
    public async Task<ActionResult<BookingConfirmationDto>> Create(CreateBookingDto bookingDto)
    {
        var confirmation = await _bookingService.CreateBookingAsync(bookingDto);

        return Ok(confirmation);
    }
}
