using Journal.Settings;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Journal.Commands;
public class AddCommand : Command<AddSettings>
{
    public override int Execute(CommandContext context, AddSettings settings)
    {
        var dateService = settings.GetDateService();

        DateOnly date;

        if (settings.Date == "today" || settings.Date == "now")
            date = DateOnly.FromDateTime(DateTime.Today);
        else
            date = DateOnly.FromDateTime(DateTime.Parse(settings.Date));

        dateService.Add(date);

        AnsiConsole.WriteLine($"added: {date}");

        return 0;
    }
}
