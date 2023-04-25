using Drones.Infrastructure.DataContext;

namespace Drones.LocalServices.BatteryLevelLogService
{
    public class BatteryLevelLogService : IBatteryLevelLogService
    {
        private readonly ILogger<BatteryLevelLogService> _logger;
        private readonly AppDBContext _context;
        public BatteryLevelLogService(ILogger<BatteryLevelLogService> logger,AppDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task Run()
        {
            var drones = _context.Drones.ToList();
            foreach (var drone in drones)
            {
                _context.BatteryLevelLogs.Add(new Infrastructure.Models.BatteryLevelLog() 
                {
                    BatteryLevel=drone.BatteryCapacity,
                    DroneId=drone.Id,
                    LoggedAt=DateTime.Now
                });
                _context.SaveChanges();
                _logger.LogInformation($"Drone Id: {drone.Id} Battery Level : {drone.BatteryCapacity}");
            }
        }
    }
}
