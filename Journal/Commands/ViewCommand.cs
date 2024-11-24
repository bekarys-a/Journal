using Journal.Extensions;
using Journal.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Globalization;
using Calendar = Spectre.Console.Calendar;

namespace Journal.Commands;

public class ViewCommand : Command<ViewSettings>
{
    public override int Execute(CommandContext context, ViewSettings settings)
    {
        var dateService = settings.GetDateService();

        var dateQuery = dateService.GetDates();

        if (settings.Last.HasValue)
            dateQuery = dateQuery.Reverse().Take(settings.Last.Value);

        if (settings.CurrentYear)
            dateQuery = dateQuery.Where(d => d.Year == DateTime.Today.Year);
        else if (settings.CurrentMonth)
            dateQuery = dateQuery.Where(d => d.Month == DateTime.Today.Month && d.Year == DateTime.Today.Year);
        else if (settings.CurrentWeek)
            dateQuery = dateQuery.Where(d =>
                ISOWeek.GetWeekOfYear(d.ToDateTime()) == ISOWeek.GetWeekOfYear(DateTime.Now) && 
                d.Year == DateTime.Today.Year
            );

        var months = dateQuery.GroupBy(d => d.Month).OrderBy(d => d.Key);

        var date = dateQuery.ToArray();

        foreach (var month in months)
        {
            var days = month.ToArray();
            var calendar = new Calendar(days[0].ToDateTime());

            calendar.MinimalBorder();

            foreach (var day in days)
                calendar.AddCalendarEvent(day.ToDateTime());

            AnsiConsole.Write(calendar);

            foreach (var day in days)
                AnsiConsole.WriteLine(day.ToString());
        }

        AnsiConsole.WriteLine($"количество дат: {date.Length}");

        return 0;
    }
}
