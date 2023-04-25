namespace Drones.LocalServices.BatteryLevelLogService
{
    public class BatteryLevelLogService : IBatteryLevelLogService
    {
        private readonly ILogger<BatteryLevelLogService> _logger;
        public BatteryLevelLogService(ILogger<BatteryLevelLogService> logger)
        {
            _logger = logger;
        }
        public async Task Run()
        {
            await Task.Delay(100);
            _logger.LogInformation(
                "Sample Service did something.");
        }
    }
}
