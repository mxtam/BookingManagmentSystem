using BookingManagmentSystem.Domain.Dtos.Hall;
using BookingManagmentSystem.Domain.Interfaces.Services;
using BookingManagmentSystem.Domain.Dtos.Booking;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagmentSystem.API.Controllers;

/// <summary>
/// Controller for halls
/// </summary>
[Route("api/halls")]
[ApiController]
public class HallController : ControllerBase
{
    private readonly IHallService _hallService;

    private readonly IBookingService _bookingService;

    public HallController(IHallService hallService, IBookingService bookingService)
    {
        _hallService = hallService;
        _bookingService = bookingService;
    }

    /// <summary>
    /// Get available halls based on the provided query parameters
    /// </summary>
    /// <param name="query">The query parameters for finding available halls.</param>
    /// <returns>A list of available halls.</returns>
    [HttpGet("available")]
    public async Task<ActionResult<IReadOnlyList<GetHallToListDto>>> GetAvailable([FromQuery] AvailableHallsQueryDto query)
    {
        var halls = await _bookingService.GetAvailableHallsAsync(query);
        return Ok(halls);
    }

    /// <summary>
    /// Get all halls with their available services
    /// </summary>
    /// <returns>List of halls with their available services</returns>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<GetHallToListDto>>> Get()
    {
        var halls = await _hallService.GetHallsListAsync();

        return Ok(halls);
    }

    /// <summary>
    /// Remove a hall by its ID
    /// </summary>
    /// <param name="id">The ID of the hall to remove</param>
    /// <returns>A message indicating the result of the removal operation</returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<string>> Delete(int id)
    {
        var result = await _hallService.RemoveHallAsync(id);

        return Ok(result);
    }

    /// <summary>
    /// Create a new hall with the provided details
    /// </summary>
    /// <param name="hallDto">The CreateHallDto containing the hall details.</param>
    /// <returns>A message about the success of the creation operation.</returns>
    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateHallDto hallDto)
    {
        var result = await _hallService.CreateHallAsync(hallDto);

        return Ok(result);
    }

    /// <summary>
    /// Update an existing hall with the provided details
    /// </summary>
    /// <param name="id">The ID of the hall to update</param>
    /// <param name="hallDto">The UpdateHallDto containing the updated hall information</param>
    /// <returns>A message about the success of the update operation</returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<string>> Update(int id, UpdateHallDto hallDto)
    {
        var result = await _hallService.UpdateHallAsync(id, hallDto);

        return Ok(result);
    }
}
