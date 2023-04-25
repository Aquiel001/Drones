namespace Drones.LocalServices.BatteryLevelLogService
{
    /// <summary>
    /// Battery Level Log service
    /// </summary>
    public interface IBatteryLevelLogService
    {
        /// <summary>
        /// Execute Service
        /// </summary>
        /// <returns></returns>
        public Task Run();
    }
}
