using BookingManagmentSystem.Domain.Exceptions;

namespace BookingManagmentSystem.BLL.Services;

public static class BookingPriceCalculator
{
    /// <summary>
    /// Defines the tariff structure for different time segments of the day, 
    /// where each tuple contains the start hour, end hour, 
    /// and the corresponding multiplier for the hourly rate.
    /// </summary>
    private static readonly (int StartHour, int EndHour, decimal Multiplier)[] Tariffs =
    [
        (6, 9, 0.90m),
        (9, 12, 1.00m),
        (12, 14, 1.15m),
        (14, 18, 1.00m),
        (18, 23, 0.80m)
    ];

    /// <summary>
    /// Calculates the rental cost based on the hourly rate and the booking time interval, 
    /// applying different tariffs for different time segments.
    /// </summary>
    /// <param name="hourlyRate">The hourly rate for the hall.</param>
    /// <param name="startTime">The start time of the booking.</param>
    /// <param name="endTime">The end time of the booking.</param>
    /// <returns>The total rental cost.</returns>
    /// <exception cref="BadRequestException">Thrown when the booking interval is invalid.</exception>
    public static decimal CalculateRentalCost(decimal hourlyRate, DateTime startTime, DateTime endTime)
    {
        BookingTimeRules.ValidateInterval(startTime, endTime);

        if (hourlyRate < 0)
        {
            throw new BadRequestException("Погодинна ціна залу не може бути від'ємною");
        }

        decimal cost = 0;
        foreach (var tariff in Tariffs)
        {
            var tariffStart = startTime.Date.AddHours(tariff.StartHour);
            var tariffEnd = startTime.Date.AddHours(tariff.EndHour);
            var segmentStart = startTime > tariffStart ? startTime : tariffStart;
            var segmentEnd = endTime < tariffEnd ? endTime : tariffEnd;

            if (segmentEnd > segmentStart)
            {
                var hours = (decimal)(segmentEnd - segmentStart).Ticks / TimeSpan.TicksPerHour;
                cost += hourlyRate * tariff.Multiplier * hours;
            }
        }

        return decimal.Round(cost, 2, MidpointRounding.AwayFromZero);
    }
}
