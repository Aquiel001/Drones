using Drones.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Drones.Infrastructure.DataContext.Data
{
    /// <summary>
    /// Db Initializer
    /// </summary>
    public static  class DbInitializer
    {
        /// <summary>
        /// Db Initialize method
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var _context=new AppDBContext(serviceProvider.GetRequiredService<DbContextOptions<AppDBContext>>()))
            {
                if (_context.Drones.Any())
                {
                    return;

                }

                _context.Drones.AddRange(
                    new Drone { BatteryCapacity=25,Model=Shared.DroneModel.Lightweight,SerialNumber="1245678",Status=Shared.DroneStatus.IDLE,Weight=100}, 
                    new Drone { BatteryCapacity=50,Model=Shared.DroneModel.Middleweight,SerialNumber="1245018",Status=Shared.DroneStatus.LOADING,Weight=200}, 
                    new Drone { BatteryCapacity=75,Model=Shared.DroneModel.Heavyweight,SerialNumber="1201678",Status=Shared.DroneStatus.LOADED,Weight=500}, 
                    new Drone { BatteryCapacity=90,Model=Shared.DroneModel.Cruiserweight,SerialNumber="1001678",Status=Shared.DroneStatus.DELIVERING,Weight=400}, 
                    new Drone { BatteryCapacity=100,Model=Shared.DroneModel.Lightweight,SerialNumber="1200278",Status=Shared.DroneStatus.DELIVERED,Weight=100}, 
                    new Drone { BatteryCapacity=10,Model=Shared.DroneModel.Middleweight,SerialNumber="1992678",Status=Shared.DroneStatus.RETURNING,Weight=200}, 
                    new Drone { BatteryCapacity=50,Model=Shared.DroneModel.Cruiserweight,SerialNumber="9925678",Status=Shared.DroneStatus.IDLE,Weight=400}, 
                    new Drone { BatteryCapacity=75,Model=Shared.DroneModel.Heavyweight,SerialNumber="12499938",Status=Shared.DroneStatus.LOADING,Weight=500}, 
                    new Drone { BatteryCapacity=90,Model=Shared.DroneModel.Lightweight,SerialNumber="12993278",Status=Shared.DroneStatus.LOADED,Weight=100}, 
                    new Drone { BatteryCapacity=100,Model=Shared.DroneModel.Middleweight,SerialNumber="1772678",Status=Shared.DroneStatus.RETURNING,Weight=300}
                    );

                _context.SaveChanges();

            }
        }
    }
}
