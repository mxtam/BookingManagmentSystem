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

    /// <summary>
    /// Remove a hall by its ID
    /// </summary>
    /// <param name="id">The ID of the hall to remove</param>
    /// <returns>A message indicating the result of the removal operation</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Delete(int id)
    {
        var result = await _hallService.RemoveHallAsync(id);

        return Ok(result);
    }
}
