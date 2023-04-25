using Drones.LocalServices.BatteryLevelLogService;

namespace Drones.LocalServices
{
    public class PeriodicHostedService : BackgroundService
    {


        private readonly IServiceScopeFactory _factory;
        private readonly ILogger<PeriodicHostedService> _logger;
        private readonly TimeSpan _period = TimeSpan.FromSeconds(30);
        private int _executionCount = 0;
        public bool IsEnabled { get; set; } = true;
        public PeriodicHostedService(ILogger<PeriodicHostedService> logger,IServiceScopeFactory factory)
        { 
            _logger = logger;

            _factory = factory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (
                !stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    if (IsEnabled)
                    {
                        await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                        IBatteryLevelLogService sampleService = asyncScope.ServiceProvider.GetRequiredService<IBatteryLevelLogService>();
                        await sampleService.Run();
                        _executionCount++;
                        _logger.LogInformation(
                            $"Executed PeriodicHostedService - Count: {_executionCount}");
                    }
                    else
                    {
                        _logger.LogInformation(
                            "Skipped PeriodicHostedService");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(
                        $"Failed to execute PeriodicHostedService with exception message {ex.Message}. Good luck next round!");
                }
            }
        }
    }
}
