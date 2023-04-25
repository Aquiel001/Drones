namespace Drones.Infrastructure.Models
{
    /// <summary>
    /// Drone with medicine relationship
    /// </summary>
    public class Load:BaseEntity
    {
        /// <summary>
        /// Drone Id
        /// </summary>
        public int DroneId { get; set; }
        /// <summary>
        /// Medication Id
        /// </summary>
        public int MedicationId { get; set; }

        /// <summary>
        /// Drone
        /// </summary>
        public Drone Drone { get; set; }=new Drone();
        /// <summary>
        /// Medication
        /// </summary>
        public Medication Medication { get; set; } = new Medication();
    }
}
