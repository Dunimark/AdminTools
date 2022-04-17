using AdminTools.Agent;

var host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options => { options.ServiceName = "KastAdminTools.Agent"; })
    .ConfigureServices(
        services =>
        {
            services.AddSingleton<JokeService>();
            services.AddHostedService<WindowsBackgroundService>();
        })
    .Build();

await host.RunAsync();