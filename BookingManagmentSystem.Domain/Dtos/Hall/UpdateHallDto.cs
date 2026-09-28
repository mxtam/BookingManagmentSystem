using System.ComponentModel.DataAnnotations;

namespace BookingManagmentSystem.Domain.Dtos.Hall;

public class UpdateHallDto
{
    [Required(ErrorMessage = "Назва залу є обов'язковою")]
    [StringLength(10, ErrorMessage = "Назва залу не може перевищувати 10 символів")]
    public required string Title { get; set; }

    [Range(1, 500, ErrorMessage = "Місткість повинна не перевищувати 500 та не бути меншою за 1")]
    public int Capacity { get; set; }

    [Range(1, 10000, ErrorMessage = "Ціна повинна не перевищувати 10000 та не бути меншою за 1")]
    public decimal Price { get; set; }

    public HashSet<int> ServiceIds { get; set; } = [];
}