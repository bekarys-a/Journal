using Journal.Commands;
using Spectre.Console.Cli;
using System.Reflection;



var app = new CommandApp();

app.Configure(config =>
{
    config.SetApplicationName("journal");
    var version = Assembly.GetEntryAssembly().GetName().Version!;
    config.SetApplicationVersion(
        string.Join('.', new int[] { version.Major, version.Minor, (int)version.Build })
    );

    config.AddCommand<ViewCommand>("view")
        .WithAlias("v")
        .WithDescription("просмотр");

    config.AddCommand<AddCommand>("add")
        .WithAlias("a")
        .WithDescription("добавить дату в файл");

    config.AddCommand<DeleteCommand>("delete")
        .WithAlias("del")
        .WithAlias("d")
        .WithDescription("удалить дату из файла");
});

return app.Run(args);
