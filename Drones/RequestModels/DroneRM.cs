using Drones.Shared;
using System.ComponentModel.DataAnnotations;

namespace Drones.RequestModels
{
    /// <summary>
    /// Drone Request Model
    /// </summary>
    public class DroneRM
    {
        /// <summary>
        /// Serial Number
        /// </summary>
        [MaxLength(100)]
        public string SerialNumber { get; set; }
        /// <summary>
        /// Drone Model
        /// </summary>
        public DroneModel Model { get; set; }

        /// <summary>
        /// Weight
        /// </summary>
        /// <remarks>  (500gr max);</remarks>
        public decimal Weight { get; set; }

        /// <summary>
        /// Battery Capacity 
        /// </summary>
        /// <remarks> Battery Capacity in Percent</remarks>
        public int BatteryCapacity { get; set; }

        /// <summary>
        /// Drone Status
        /// </summary>
        /// <seealso cref="DroneStatus"/>
        public DroneStatus Status { get; set; }
    }
}
