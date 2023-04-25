using Drones.Infrastructure.Models;

namespace Drones.RequestModels
{
    public class MedicationRM
    {

        public string Name { get; set; }
        public decimal Weight { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }
    }
}
