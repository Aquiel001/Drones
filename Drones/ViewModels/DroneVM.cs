using Drones.Infrastructure.Models;
using Drones.Shared;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace Drones.ViewModels
{
    /// <summary>
    /// Drone View Model
    /// </summary>
    public class DroneVM
    {
        /// <summary>
        /// Drone Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Serial Number
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// Drone Model
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Weight
        /// </summary>
        /// <remarks>  (500gr max);</remarks>
        public string Weight { get; set; }
        public string AvailableWeight { get; set; }


        /// <summary>
        /// Battery Capacity 
        /// </summary>
        /// <remarks> Battery Capacity in Percent</remarks>
        public string BatteryCapacity { get; set; }

        /// <summary>
        /// Drone Status
        /// </summary>
        public string Status { get; set; }

        public List<MedicationVM> Medications { get; set; }


        public static DroneVM FromDrone(Drone drone)
        {
            return new DroneVM 
            {
                Id = drone.Id,
                BatteryCapacity=string.Format("{0} %",drone.BatteryCapacity),
                Model=drone.Model.ToString(),
                SerialNumber=drone.SerialNumber,
                Status=drone.Status.ToString(),
                Weight =string.Format("{0} gm",decimal.Round( drone.Weight,2)),
                AvailableWeight =string.Format("{0} gm",decimal.Round( drone.AvailableWeight,2)),
                Medications=drone.Medications.Select<Medication,MedicationVM>(x=>new MedicationVM()
                {
                    Id = x.Id,
                    Code=x.Code,
                    Image=x.Image,
                    Name=x.Name,
                    Weight=x.Weight
                }).ToList()
            };
        }
    }
}
