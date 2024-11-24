using Spectre.Console.Cli;
using System.ComponentModel;

namespace Journal.Settings;
public class DeleteSettings : FilePathSettings
{
    [CommandArgument(1, "<DATE>")]
    [Description("Дата для удаление. Слова 'now' или 'today' будут преобразованы в текущий день")]
    public string Date { get; set; }

    [CommandOption("-a|--all")]
    [Description("Удалить все совопадающие")]
    public bool RemoveAll { get; set; }
}
