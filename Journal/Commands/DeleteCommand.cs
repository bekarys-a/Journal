using Journal.Settings;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Journal.Commands;
public class DeleteCommand : Command<DeleteSettings>
{
    public override int Execute(CommandContext context, DeleteSettings settings)
    {
        var dateService = settings.GetDateService();

        DateOnly date;

        if (settings.Date == "today" || settings.Date == "now")
            date = DateOnly.FromDateTime(DateTime.Today);
        else
            date = DateOnly.FromDateTime(DateTime.Parse(settings.Date));

        int deleted;

        if (settings.RemoveAll)
            deleted = dateService.RemoveAll(date);
        else
            deleted = dateService.Remove(date) ? 1 : 0;

        AnsiConsole.WriteLine($"deleted {date}: {deleted}");

        return 0;
    }
}
