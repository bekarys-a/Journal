using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace Journal.Settings;

public class FilePathSettings : CommandSettings
{

    [CommandArgument(0, "<PATH>")]
    [Description("Путь до файла с датами")]
    public string Path { get; set; }

    public DateService GetDateService()
    {
        var dateFile = new DateFile(Path);
        return new DateService(dateFile);
    }

    public override ValidationResult Validate()
    {
        if (!File.Exists(Path))
            return ValidationResult.Error($"Path not found - {Path}");

        return ValidationResult.Success();
    }
}
