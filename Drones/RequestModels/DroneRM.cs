using Drones.Shared;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [Range(0,500)]
        public decimal Weight { get; set; }

        /// <summary>
        /// Battery Capacity 
        /// </summary>
        /// <remarks> Battery Capacity in Percent</remarks>
        [Range(0,100)]
        public int BatteryCapacity { get; set; }

        /// <summary>
        /// Drone Status
        /// </summary>
        public DroneStatus Status { get; set; }
    }
}
