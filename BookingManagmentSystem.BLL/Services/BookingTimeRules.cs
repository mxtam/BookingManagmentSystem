using BookingManagmentSystem.Domain.Exceptions;

namespace BookingManagmentSystem.BLL.Services;

internal static class BookingTimeRules
{
    /// <summary>
    /// Calculates the end time of a booking based on the provided start time and duration in minutes.
    /// </summary>
    /// <param name="startTime">The start time of the booking.</param>
    /// <param name="durationMinutes">The duration of the booking in minutes.</param>
    /// <returns>The end time of the booking.</returns>
    /// <exception cref="BadRequestException">Thrown when the duration is invalid.</exception>
    public static DateTime GetEndTime(DateTime startTime, int durationMinutes)
    {
        if (durationMinutes < 1 || durationMinutes > 1020)
        {
            throw new BadRequestException("Тривалість бронювання повинна бути від 1 до 1020 хвилин");
        }

        if (startTime > DateTime.MaxValue.AddMinutes(-durationMinutes))
        {
            throw new BadRequestException("Дата завершення бронювання виходить за допустимі межі");
        }

        var endTime = startTime.AddMinutes(durationMinutes);
        ValidateInterval(startTime, endTime);
        return endTime;
    }

    /// <summary>
    /// Validates the provided start and end times for a booking interval.
    /// </summary>
    /// <param name="startTime">The start time of the booking.</param>
    /// <param name="endTime">The end time of the booking.</param>
    /// <exception cref="BadRequestException">Thrown when the interval is invalid.</exception>
    public static void ValidateInterval(DateTime startTime, DateTime endTime)
    {
        if (startTime == default || endTime == default || endTime <= startTime)
        {
            throw new BadRequestException("Вкажіть дату й час початку та завершення; завершення повинно бути пізніше початку");
        }

        if (startTime.Kind != DateTimeKind.Unspecified || endTime.Kind != DateTimeKind.Unspecified)
        {
            throw new BadRequestException("Вкажіть місцевий час залу без часового поясу або без суфікса Z");
        }

        if (startTime.Date != endTime.Date ||
            startTime.TimeOfDay < TimeSpan.FromHours(6) ||
            endTime.TimeOfDay > TimeSpan.FromHours(23))
        {
            throw new BadRequestException("Бронювання доступне лише з 06:00 до 23:00 в межах одного дня");
        }
    }
}
