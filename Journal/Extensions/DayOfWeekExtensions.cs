using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal.Extensions;


internal static class DayOfWeekExtensions
{
    internal static string GetAbbreviatedDayName(this DayOfWeek day, CultureInfo? culture = null)
    {
        culture ??= CultureInfo.InvariantCulture;
        return culture.DateTimeFormat
            .GetAbbreviatedDayName(day)
            .CapitalizeFirstLetter(culture);
    }

    internal static DayOfWeek GetNextWeekDay(this DayOfWeek day)
    {
        var next = (int)day + 1;
        if (next > (int)DayOfWeek.Saturday)
        {
            return DayOfWeek.Sunday;
        }

        return (DayOfWeek)next;
    }
}
