using Microsoft.VisualStudio.TestTools.UnitTesting;
using Drones.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using DronesTests.Infrastructure;
using Drones.ViewModels;
using DescriptionAttribute = Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute;
using System.ComponentModel.DataAnnotations;
using Bogus;
using Drones.Infrastructure.Models;
using Drones.Shared;
using Drones.RequestModels;
using Microsoft.EntityFrameworkCore;
using Bogus.Extensions.UnitedKingdom;

namespace Drones.Controllers.Tests
{
    [TestClass()]
    public class DispatchControllerTests:BaseContextTest
    {
        DispatchController _controller;
        public DispatchControllerTests()
        {
             _controller = Container.GetService<DispatchController>();


        }
        //public DispatchController _controller => Container.GetService<DispatchController>();


        [TestMethod()]
        
        public void GetDronesTest()
        {

           var response= _controller.GetDrones();
            Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)response.Result).StatusCode);
        }

        [TestMethod()]
        public void GetDroneOkTest()
        {
            var response = _controller.GetDrone(1);
            Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)response.Result).StatusCode);
            Assert.IsNotNull(((Microsoft.AspNetCore.Mvc.ObjectResult)response.Result).Value);
            Assert.IsTrue(1== ((Drones.ViewModels.DroneVM)((Microsoft.AspNetCore.Mvc.ObjectResult)response.Result).Value).Id);
        }
        [TestMethod()]
        public void GetDroneNotFoundTest()
        {
            var response = _controller.GetDrone(20);
            Assert.AreEqual(404, ((Microsoft.AspNetCore.Mvc.ObjectResult)response.Result).StatusCode);
            
        }

        [TestMethod()]
        public void RegisterDroneOkTest()
        {
           Assert.IsTrue(10==  _context.Drones.Count());
            var response = _controller.RegisterDrone(new Faker<DroneRM>()
                           .RuleFor(v => v.Status, DroneStatus.IDLE)
                           .RuleFor(v => v.BatteryCapacity, 100)
                           .RuleFor(v => v.Model, f => f.PickRandom<DroneModel>())
                           .RuleFor(v => v.SerialNumber, f => f.Internet.Mac())
                           .RuleFor(v => v.Weight, 500)
                           .Generate());
            Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)response).StatusCode);
           Assert.IsTrue(11==  _context.Drones.Count());

        }
        


        [TestMethod()]
        public void LoadDroneOkwith500Test()
        {
            Assert.IsTrue(0 == _context.Medications.Count(x=>x.DroneId==1));
            var drone = _context.Drones.Where(x => x.Id == 1).First();
            Assert.IsTrue(500 == drone.AvailableWeight);
            Assert.IsTrue(DroneStatus.IDLE == drone.Status);

            var response = _controller.LoadDrone(1,new Faker<MedicationRM>()
                           .RuleFor(v => v.Name, f=>f.Commerce.ProductName())
                           .RuleFor(v => v.Code, f=>f.Finance.SortCode().ToUpper())
                           .RuleFor(v => v.Image, f => f.Image.Abstract())
                           .RuleFor(v => v.Weight,500)
                           .Generate());

            Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)response).StatusCode);
            Assert.IsTrue(1 == _context.Medications.Count(x => x.DroneId == 1));
            Assert.IsTrue(0 == _context.Drones.Where(x => x.Id == 1).First().AvailableWeight);
            Assert.IsTrue(DroneStatus.LOADED == _context.Drones.Where(x => x.Id == 1).First().Status);


        }

        [TestMethod()]
        public void LoadDroneDroneNotFoundTest()
        {

            var response = _controller.LoadDrone(20, new Faker<MedicationRM>()
                           .RuleFor(v => v.Name, f => f.Commerce.ProductName())
                           .RuleFor(v => v.Code, f => f.Finance.SortCode().ToUpper())
                           .RuleFor(v => v.Image, f => f.Image.Abstract())
                           .RuleFor(v => v.Weight, 500)
                           .Generate());
            Assert.AreEqual(404, ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
            Assert.AreEqual("Drone not Found", ((Microsoft.AspNetCore.Mvc.ObjectResult)response).Value);

        }

        [TestMethod()]
        public void LoadDroneDroneOverLoadTest()
        {
            Assert.IsTrue(0 == _context.Medications.Count(x => x.DroneId == 1));
            var drone = _context.Drones.Where(x => x.Id == 1).First();
            Assert.IsTrue(500 == drone.AvailableWeight);
            Assert.IsTrue(DroneStatus.IDLE == drone.Status);

            var response = _controller.LoadDrone(1, new Faker<MedicationRM>()
                           .RuleFor(v => v.Name, f => f.Commerce.ProductName())
                           .RuleFor(v => v.Code, f => f.Finance.SortCode().ToUpper())
                           .RuleFor(v => v.Image, f => f.Image.Abstract())
                           .RuleFor(v => v.Weight, 600)
                           .Generate());
            Assert.AreEqual(400, ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
            Assert.AreEqual("The maximum load of the drone is 500 gr", ((Microsoft.AspNetCore.Mvc.ObjectResult)response).Value);
            Assert.IsTrue(0 == _context.Medications.Count(x => x.DroneId == 1));
            Assert.IsTrue(500 == _context.Drones.Where(x => x.Id == 1).First().AvailableWeight);
            Assert.IsTrue(DroneStatus.IDLE == _context.Drones.Where(x => x.Id == 1).First().Status);

        }

        [TestMethod()]
        public void LoadDroneDroneLoadSeveralTimesTest()
        {
            Assert.IsTrue(0 == _context.Medications.Count(x => x.DroneId == 1));

            var drone = _context.Drones.Where(x => x.Id == 1).First();
            Assert.IsTrue(500 == drone.AvailableWeight);
            Assert.IsTrue(DroneStatus.IDLE == drone.Status);

            var response = _controller.LoadDrone(1, new Faker<MedicationRM>()
                           .RuleFor(v => v.Name, f => f.Commerce.ProductName())
                           .RuleFor(v => v.Code, f => f.Finance.SortCode().ToUpper())
                           .RuleFor(v => v.Image, f => f.Image.Abstract())
                           .RuleFor(v => v.Weight, 100)
                           .Generate());

            //Assert.AreEqual(400, ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
            Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)response).StatusCode);
            Assert.IsTrue(1 == _context.Medications.Count(x => x.DroneId == 1));
            Assert.IsTrue(400 == _context.Drones.Where(x => x.Id == 1).First().AvailableWeight);
            Assert.IsTrue(DroneStatus.LOADING == _context.Drones.Where(x => x.Id == 1).First().Status);

            response = _controller.LoadDrone(1, new Faker<MedicationRM>()
                          .RuleFor(v => v.Name, f => f.Commerce.ProductName())
                          .RuleFor(v => v.Code, f => f.Finance.SortCode().ToUpper())
                          .RuleFor(v => v.Image, f => f.Image.Abstract())
                          .RuleFor(v => v.Weight, 100)
                          .Generate());

            Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)response).StatusCode);
            Assert.IsTrue(2 == _context.Medications.Count(x => x.DroneId == 1));
            Assert.IsTrue(300 == _context.Drones.Where(x => x.Id == 1).First().AvailableWeight);
            Assert.IsTrue(DroneStatus.LOADING == _context.Drones.Where(x => x.Id == 1).First().Status);

            response = _controller.LoadDrone(1, new Faker<MedicationRM>()
                          .RuleFor(v => v.Name, f => f.Commerce.ProductName())
                          .RuleFor(v => v.Code, f => f.Finance.SortCode().ToUpper())
                          .RuleFor(v => v.Image, f => f.Image.Abstract())
                          .RuleFor(v => v.Weight, 400)
                          .Generate());

            Assert.AreEqual("The maximum load of the drone is 500 gr", ((Microsoft.AspNetCore.Mvc.ObjectResult)response).Value);
            Assert.IsTrue(2 == _context.Medications.Count(x => x.DroneId == 1));
            Assert.IsTrue(300 == _context.Drones.Where(x => x.Id == 1).First().AvailableWeight);
            Assert.IsTrue(DroneStatus.LOADING == _context.Drones.Where(x => x.Id == 1).First().Status);

            response = _controller.LoadDrone(1, new Faker<MedicationRM>()
                         .RuleFor(v => v.Name, f => f.Commerce.ProductName())
                         .RuleFor(v => v.Code, f => f.Finance.SortCode().ToUpper())
                         .RuleFor(v => v.Image, f => f.Image.Abstract())
                         .RuleFor(v => v.Weight, 300)
                         .Generate());

            Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)response).StatusCode);
            Assert.IsTrue(3 == _context.Medications.Count(x => x.DroneId == 1));
            Assert.IsTrue(0 == _context.Drones.Where(x => x.Id == 1).First().AvailableWeight);
            Assert.IsTrue(DroneStatus.LOADED == _context.Drones.Where(x => x.Id == 1).First().Status);

            

        }

        [TestMethod()]
        public void LoadDroneDroneInvalidStatusTest()
        {
            Assert.IsTrue(0 == _context.Medications.Count(x => x.DroneId == 1));
            var drone = _context.Drones.Where(x => x.Id == 1).First();
            Assert.IsTrue(500 == drone.AvailableWeight);
            Assert.IsTrue(DroneStatus.IDLE == drone.Status);

            drone.Status = DroneStatus.DELIVERING;
            _context.Drones.Update(drone);
            _context.SaveChanges();

            var response = _controller.LoadDrone(1, new Faker<MedicationRM>()
                           .RuleFor(v => v.Name, f => f.Commerce.ProductName())
                           .RuleFor(v => v.Code, f => f.Finance.SortCode().ToUpper())
                           .RuleFor(v => v.Image, f => f.Image.Abstract())
                           .RuleFor(v => v.Weight, 100)
                           .Generate());

            Assert.AreEqual(400, ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
            Assert.AreEqual("The drone is not available", ((Microsoft.AspNetCore.Mvc.ObjectResult)response).Value);
            Assert.IsTrue(0 == _context.Medications.Count(x => x.DroneId == 1));
            Assert.IsTrue(500 == _context.Drones.Where(x => x.Id == 1).First().AvailableWeight);
            Assert.IsTrue(DroneStatus.DELIVERING == _context.Drones.Where(x => x.Id == 1).First().Status);

        }

        [TestMethod()]
        public void LoadDroneDroneBatteryLowTest()
        {
            Assert.IsTrue(0 == _context.Medications.Count(x => x.DroneId == 1));
            var drone = _context.Drones.Where(x => x.Id == 1).First();
            Assert.IsTrue(500 == drone.AvailableWeight);
            Assert.IsTrue(DroneStatus.IDLE == drone.Status);

            drone.BatteryCapacity = 10;
            _context.Drones.Update(drone);
            _context.SaveChanges();

            var response = _controller.LoadDrone(1, new Faker<MedicationRM>()
                           .RuleFor(v => v.Name, f => f.Commerce.ProductName())
                           .RuleFor(v => v.Code, f => f.Finance.SortCode().ToUpper())
                           .RuleFor(v => v.Image, f => f.Image.Abstract())
                           .RuleFor(v => v.Weight, 100)
                           .Generate());

            Assert.AreEqual(400, ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
            Assert.AreEqual("Battery below 25%", ((Microsoft.AspNetCore.Mvc.ObjectResult)response).Value);
            Assert.IsTrue(0 == _context.Medications.Count(x => x.DroneId == 1));
            Assert.IsTrue(500 == _context.Drones.Where(x => x.Id == 1).First().AvailableWeight);
            Assert.IsTrue(DroneStatus.IDLE == _context.Drones.Where(x => x.Id == 1).First().Status);

        }


        [TestMethod()]
        public void GetAvailableDronesTest()
        {
            var response = _controller.GetAvailableDrones();
            Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)response.Result).StatusCode);
            Assert.AreEqual(10, ((List<DroneVM>)((Microsoft.AspNetCore.Mvc.ObjectResult)response.Result).Value).Count());

            var drone1 = _context.Drones.Find(1);
            drone1.BatteryCapacity = 10;
            _context.Drones.Update(drone1);
            var drone2 = _context.Drones.Find(2);
            drone2.Status = DroneStatus.DELIVERING;
            _context.Drones.Update(drone2);

            var drone3 = _context.Drones.Find(3);
            drone3.AvailableWeight = 0;
            _context.Drones.Update(drone3);

            _context.SaveChanges();

            

            response = _controller.GetAvailableDrones();
            Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)response.Result).StatusCode);
            Assert.AreEqual(7, ((List<DroneVM>)((Microsoft.AspNetCore.Mvc.ObjectResult)response.Result).Value).Count());

        }

        [TestMethod()]
        public void GetDroneBatteryLevelOkTest()
        {
            var response = _controller.GetDroneBatteryLevel(1);
            Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)response.Result).StatusCode);
            Assert.AreEqual(100, ((BatteryLevelVM)((Microsoft.AspNetCore.Mvc.ObjectResult)response.Result).Value).BatteryLevel);
        }

        [TestMethod()]
        public void GetDroneBatteryLevelNotFoundTest()
        {
            var response = _controller.GetDroneBatteryLevel(20);
            Assert.AreEqual(404, ((Microsoft.AspNetCore.Mvc.ObjectResult)response.Result).StatusCode);
        }
    }
}