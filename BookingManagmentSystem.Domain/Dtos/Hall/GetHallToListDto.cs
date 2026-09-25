using BookingManagmentSystem.Domain.Dtos.Service;

namespace BookingManagmentSystem.Domain.Dtos.Hall;

public class GetHallToListDto
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public int Capacity { get; set; }

    public decimal Price { get; set; }

    public IEnumerable<GetServiceToListDto> AvailableServices { get; set; } = new List<GetServiceToListDto>();
}
