using Drones.Infrastructure.DataContext;
using Drones.Infrastructure.Models;
using Drones.RequestModels;
using Drones.Shared;
using Drones.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Drones.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]

    [ApiController]
    [Route("/api/dispatch")]
    public class DispatchController : ControllerBase
    {
        


        private readonly ILogger<DispatchController> _logger;
        private readonly AppDBContext _dbContext;

        public DispatchController(ILogger<DispatchController> logger,AppDBContext dBContext)
        {
            _logger = logger;
            _dbContext= dBContext;
        }

        /// <summary>
        /// Get all Drones
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<List<DroneVM>> GetDrones()
        {
           var drones= _dbContext.Drones.Include(x=>x.Medications).ToList();
            return Ok( drones.Select<Drone,DroneVM>(x=>DroneVM.FromDrone(x)));
        }

        /// <summary>
        /// Get Drone By Id
        /// </summary>
        /// <param name="id">Drone Id</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [Route("{id}")]
        [HttpGet]
        public ActionResult<DroneVM> GetDrone(int id)
        {
            var drone = _dbContext.Drones.Include(x=>x.Medications).Where(x=>x.Id==id).FirstOrDefault();
            if(drone==null)
                return NotFound("Drone not Found");
            return Ok( DroneVM.FromDrone(drone));
        }

        /// <summary>
        /// Register a Drone
        /// </summary>
        /// <param name="drone"><seealso cref="DroneRM"/></param>
        /// <remarks>Status {IDLE=0,LOADING=1,LOADED=2,DELIVERING=3,DELIVERED=4,RETURNING=5} \
        /// Model {Lightweight=0,Middleweight = 1,Cruiserweight = 2,Heavyweight = 3}</remarks>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public IActionResult RegisterDrone(DroneRM drone)
        {
            _dbContext.Drones.Add(new Drone() 
            {
                BatteryCapacity=drone.BatteryCapacity,
                Model=drone.Model,
                SerialNumber=drone.SerialNumber,
                Status=drone.Status,
                Weight=drone.Weight
            });
            _dbContext.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Load a Drone
        /// </summary>
        /// <param name="id"></param>
        /// <param name="medication"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        [HttpPut]
        public IActionResult LoadDrone(int id, MedicationRM medication)
        {
            var drone = _dbContext.Drones.Find(id);
            if (drone == null) return NotFound("Drone not Found");
            _dbContext.Medications.Add(new Medication()
            {
                Code = medication.Code,
                DroneId = id,
                Image = medication.Image,
                Name = medication.Name,
                Weight = medication.Weight
            });
            _dbContext.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Get all Available Drones
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Available")]
        [HttpGet]
        public ActionResult<List<DroneVM>> GetAvailableDrones()
        {
            var drones = _dbContext.Drones.Include(x => x.Medications).Where(x=>x.BatteryCapacity>=25 && x.AvailableWeight>0).ToList();
            return Ok(drones.Select<Drone, DroneVM>(x => DroneVM.FromDrone(x)));
        }

        /// <summary>
        /// Get Drone Battery level
        /// </summary>
        /// <param name="id">Drone Id</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("battery_level/{id}")]
        [HttpGet]
        public ActionResult<DroneVM> GetDroneBatteryLevel(int id)
        {
            var drone = _dbContext.Drones.Find(id);
            if (drone == null)
                return NotFound("Drone not Found");
            return Ok(new BatteryLevelVM() {BatteryLevel=drone.BatteryCapacity });
        }
    }
}