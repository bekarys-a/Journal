using Journal.Commands;
using Spectre.Console.Cli;



var app = new CommandApp();

app.Configure(config =>
{
    config.AddCommand<ViewCommand>("view")
        .WithAlias("v")
        .WithDescription("прсмотр");

    config.AddCommand<AddCommand>("add")
        .WithAlias("a")
        .WithDescription("добавить дату в файл");

    config.AddCommand<DeleteCommand>("delete")
        .WithAlias("del")
        .WithAlias("d")
        .WithDescription("удалить дату из файла");
});

return app.Run(args);
