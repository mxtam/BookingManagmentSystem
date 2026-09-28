using BookingManagmentSystem.Domain.Dtos.Service;
using BookingManagmentSystem.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagmentSystem.API.Controllers
{
    /// <summary>
    /// Controller for services
    /// </summary>
    [Route("api/services")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        /// <summary>
        /// Get all services
        /// </summary>
        /// <returns>List of services</returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetServiceToListDto>>> Get()
        {
            var services = await _serviceService.GetServicesListAsync();
            return Ok(services);
        }
    }
}
