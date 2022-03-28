using System;
using congestion.calculator;
public class CongestionTaxCalculator
{
    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */

    public int GetTax(Vehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        int tempFee = 0;
        var hour = TimeSpan.FromHours(1);
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);

            if (date - intervalStart <= hour)
            {
                tempFee = Math.Max(tempFee, nextFee);
            }
            else
            {
                totalFee += tempFee;
                tempFee = nextFee;
                intervalStart = date;
            }
        }
        return Math.Max(60, totalFee + tempFee);
    }

    public int GetTaxMyDecision(Vehicle vehicle, DateTime[] dates)
    {
        if (IsTollFreeVehicle(vehicle))
        {
            return 0;
        }
        var hour = TimeSpan.FromHours(1);
        var maxFee = 60;
        var result = dates.Where(date => !IsTollFreeDate(date))
            .OrderBy(date => date)
            .Aggregate(new TollTaxModel(),
            (model, date) =>
            {
                if (date - model.LastTaxTime > hour)
                {
                    model.LastTaxTime = date;
                    model.Sum += model.LastTax;
                    model.LastTax = GetTollFee(date, vehicle);
                }
                else
                {
                    model.LastTax = Math.Max(model.LastTax, GetTollFee(date, vehicle));
                }
                return model;
            },
            model => Math.Min(maxFee, model.LastTax + model.Sum));
        return result;
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;
        string vehicleType = vehicle.GetVehicleType();
        return Enum.GetNames<TollFreeVehicles>().Any(name => name == vehicleType);
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
        else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
        else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }

    private bool IsTollFreeDate(DateTime date)
    {
        var isWeekend = CheckIsWeekend(date);
        var isSpecialMonth = CheckIsSpecialMonth(date);
        var isHoliday = CheckIsHoliday(date);

        return isHoliday || isWeekend || isSpecialMonth;
    }
    private bool CheckIsHoliday(DateTime date)
    {
        var holidays = new HashSet<DateOnly>()
            {
                new DateOnly(2013, 1, 1),
                new DateOnly(2013, 3, 28),
                new DateOnly(2013, 3, 29),
                new DateOnly(2013, 4, 1),
                new DateOnly(2013, 4, 30),
                new DateOnly(2013, 5, 1),
                new DateOnly(2013, 5, 8),
                new DateOnly(2013, 5, 9),
                new DateOnly(2013, 6, 5),
                new DateOnly(2013, 6, 6),
                new DateOnly(2013, 6, 21),
                new DateOnly(2013, 11, 1),
                new DateOnly(2013, 12, 24),
                new DateOnly(2013, 12, 25),
                new DateOnly(2013, 12, 26),
                new DateOnly(2013, 12, 31)
            };
        return holidays.Contains(DateOnly.FromDateTime(date));
    }
    private bool CheckIsWeekend(DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday; 
    }

    private bool CheckIsSpecialMonth(DateTime date)
    {
        var specialMoths = new HashSet<int>() { 7 };
        return specialMoths.Contains(date.Month);
    }

    private enum TollFreeVehicles
    {
        Motorcycle = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }

    private class TollTaxModel
    {
        public int LastTax { get; set; }

        public int Sum { get; set; }

        public DateTime LastTaxTime { get; set; }
    }
}