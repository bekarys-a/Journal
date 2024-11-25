using Spectre.Console.Cli;
using System.ComponentModel;

namespace Journal.Settings;


public class ViewSettings : FilePathSettings
{
    [CommandOption("-l|--last")]
    [Description("Количество последних строк")]
    public int? Last { get; set; }

    [CommandOption("-c|--column")]
    [Description("Количество колонк месяца")]
    [DefaultValue(0)]
    public int Column { get; set; }

    [CommandOption("-y|--current-year")]
    [Description("показать за текущий год")]
    public bool CurrentYear { get; set; }

    [CommandOption("-m|--current-month")]
    [Description("показать за текущий месяц")]
    public bool CurrentMonth { get; set; }

    [CommandOption("-w|--current-week")]
    [Description("показать за текущую неделю")]
    public bool CurrentWeek { get; set; }
}
