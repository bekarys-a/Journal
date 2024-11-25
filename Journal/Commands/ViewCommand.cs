using Journal.Extensions;
using Journal.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Rendering;
using System.Globalization;
using System.Text.RegularExpressions;
using Calendar = Spectre.Console.Calendar;

namespace Journal.Commands;

public class ViewCommand : Command<ViewSettings>
{
    public override int Execute(CommandContext context, ViewSettings settings)
    {
        if (settings.CurrentWeek)
            Week(context, settings);
        else
            Month(context, settings);

        return 0;
    }

    public void Month(CommandContext context, ViewSettings settings)
    {
        var dateService = settings.GetDateService();
        var dateQuery = dateService.GetDates();

        if (settings.Last.HasValue)
            dateQuery = dateQuery.Reverse().Take(settings.Last.Value);

        if (settings.CurrentYear)
            dateQuery = dateQuery.Where(d => d.Year == DateTime.Today.Year);
        else if (settings.CurrentMonth)
            dateQuery = dateQuery.Where(d => d.Month == DateTime.Today.Month && d.Year == DateTime.Today.Year);;

        DrawCalendars(context, settings, dateQuery);

        var date = dateQuery.ToArray();
        foreach (var day in date)
            AnsiConsole.WriteLine(day.ToString());

        AnsiConsole.WriteLine($"количество дат: {date.Length}");
    }

    public void DrawCalendars(CommandContext context, ViewSettings settings, IEnumerable<DateOnly> dateQuery)
    {
        var monthGroup = dateQuery
            .GroupBy(d => d.Month)
            .OrderBy(d => d.Key)
            .Select((value, index) => new { value, index })
            .GroupBy(x => x.index / 3)
            .Select(g => g.Select(x => x.value).ToArray())
            .ToArray();

        var table = new Table();
        table.HideHeaders();

        foreach (var i in Enumerable.Range(0, settings.Column))
        {
            table.AddColumn("");
            table.Columns[i].Padding(0, 0);
        }

        foreach (var months in monthGroup)
        {
            var row = new List<IRenderable>();

            foreach (var month in months)
            {
                var days = month.ToArray();
                var calendar = new Calendar(days[0].ToDateTime());

                calendar.MinimalBorder();

                foreach (var day in days)
                    calendar.AddCalendarEvent(day.ToDateTime());

                row.Add(calendar);
            }

            table.AddRow(row);
        }

        AnsiConsole.Write(table);
    }

    public void Week(CommandContext context, ViewSettings settings)
    {
        var dateService = settings.GetDateService();
        var dateQuery = dateService.GetDates();

        if (settings.Last.HasValue)
            dateQuery = dateQuery.Reverse().Take(settings.Last.Value);

        dateQuery = dateQuery.Where(d =>
            ISOWeek.GetWeekOfYear(d.ToDateTime()) == ISOWeek.GetWeekOfYear(DateTime.Now) &&
            d.Year == DateTime.Today.Year
        );

        var date = dateQuery.ToArray();

        var table = new Table();

        var heading = DateTime.Today.ToString("Y", CultureInfo.InvariantCulture).EscapeMarkup();
        table.Title = new TableTitle(heading);

        foreach (var order in GetWeekdays())
            table.AddColumn(new TableColumn(order.GetAbbreviatedDayName()));

        var row = new List<IRenderable>();

        var week = ISOWeek.GetWeekOfYear(DateTime.Today);
        foreach (var day in GetNumberDays(DateTime.Today.Year, week))
        {
            if (date.Any(i => i.Day == day))
                row.Add(new Markup(day.ToString(CultureInfo.InvariantCulture) + "*", new Style(Color.Blue)));
            else
                row.Add(new Text(day.ToString(CultureInfo.InvariantCulture)));
        }

        table.AddRow(row);

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine($"количество дат: {date.Length}");
    }

    private int[] GetNumberDays(int year, int week)
    {
        DateTime startOfWeek = ISOWeek.ToDateTime(year, week, new CultureInfo("ru-ru").DateTimeFormat.FirstDayOfWeek);

        var days = new int[7];
        foreach (int i in Enumerable.Range(0, 7))
            days[i] = startOfWeek.AddDays(i).Day;

        return [..days];
    }

    private DayOfWeek[] GetWeekdays()
    {
        var days = new DayOfWeek[7];
        days[0] = CultureInfo.InvariantCulture.DateTimeFormat.FirstDayOfWeek;
        for (var i = 1; i < 7; i++)
        {
            days[i] = days[i - 1].GetNextWeekDay();
        }

        return days;
    }
}
