namespace AdminTools.Agent;

public class WindowsBackgroundService : BackgroundService
{
    private readonly JokeService _jokeService;
    private readonly ILogger<WindowsBackgroundService> _logger;

    public WindowsBackgroundService(
        JokeService jokeService,
        ILogger<WindowsBackgroundService> logger)
    {
        (_jokeService, _logger) = (jokeService, logger);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _jokeService.CreateJson();
            var joke = _jokeService.GetJoke();
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            _logger.LogWarning(joke);
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}