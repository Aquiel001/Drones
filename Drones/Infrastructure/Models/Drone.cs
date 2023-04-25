using Drones.Shared;
using System.ComponentModel.DataAnnotations;

namespace Drones.Infrastructure.Models
{
    public class Drone:BaseEntity
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
        ///  Available Weight
        /// </summary>
        /// <remarks>  (500gr max);</remarks>
        public decimal AvailableWeight { get; set; }

        /// <summary>
        /// Battery Capacity 
        /// </summary>
        /// <remarks> Battery Capacity in Percent</remarks>
        public int BatteryCapacity { get; set; }

        /// <summary>
        /// Drone Status
        /// </summary>
        public DroneStatus Status { get; set; }

        public virtual ICollection<Medication> Medications { get; set; }
        public virtual ICollection<BatteryLevelLog> BatteryLevelLogs { get; set; }
    }
}
