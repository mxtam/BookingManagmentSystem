using BookingManagmentSystem.Domain.Dtos.Hall;
using BookingManagmentSystem.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagmentSystem.API.Controllers;

/// <summary>
/// Controller for halls
/// </summary>
[Route("api/hall")]
[ApiController]
public class HallController : ControllerBase
{
    private readonly IHallService _hallService;

    public HallController(IHallService hallService)
    {
        _hallService = hallService;
    }

    /// <summary>
    /// Get all halls with their available services
    /// </summary>
    /// <returns>List of halls with their available services</returns>
    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyList<GetHallToListDto>>> Get()
    {
        var halls = await _hallService.GetHallsListAsync();

        return Ok(halls);
    }
}
