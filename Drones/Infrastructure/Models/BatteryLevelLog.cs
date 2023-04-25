using System.ComponentModel.DataAnnotations.Schema;

namespace Drones.Infrastructure.Models
{
    public class BatteryLevelLog:BaseEntity
    {
        public DateTime LoggedAt { get; set; }
        public int BatteryLevel { get; set; }
        
        public int DroneId { get; set; }

        [ForeignKey("DroneId")]
        public Drone Drone { get; set; }
    }
}
