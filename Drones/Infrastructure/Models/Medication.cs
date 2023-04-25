using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drones.Infrastructure.Models
{
    public class Medication:BaseEntity
    {
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }
        public int DroneId { get; set; }
        [ForeignKey("DroneId")]
        public Drone Drone { get; set; }
    }
}
