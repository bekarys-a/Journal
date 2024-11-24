using Spectre.Console.Cli;
using System.ComponentModel;

namespace Journal.Settings;
public class AddSettings : FilePathSettings
{
    [CommandArgument(1, "<DATE>")]
    [Description("Дата для добавление. Слова 'now' или 'today' будут преобразованы в текущий день")]
    public string Date { get; set; }
}
