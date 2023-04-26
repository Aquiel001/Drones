using Bogus;
using Drones.Infrastructure.DataContext;
using Drones.Infrastructure.Models;
using Drones.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesTests.Infrastructure
{
    public class DbInitializerTest
    {
        public static void Initialize(AppDBContext context, Microsoft.Extensions.DependencyInjection.ServiceCollection services)
        {
            if(context.Drones.Any()) { return; }

            Seed(context);
            services.AddTransient<AppDBContext>(c => context);


        }

        private static void Seed(AppDBContext context)
        {
            var drones = new Drone[10];

            for (int i = 0; i < 10; i++)
            {


               drones[i]= new Faker<Drone>()
                           .RuleFor(v => v.Status, DroneStatus.IDLE)
                           .RuleFor(v => v.AvailableWeight, 500)
                           .RuleFor(v => v.BatteryCapacity, 100)
                           .RuleFor(v => v.Model, f => f.PickRandom<DroneModel>())
                           .RuleFor(v => v.SerialNumber, f => f.Internet.Mac())
                           .RuleFor(v => v.Weight, 500)
                           .Generate();
            }
                context.Drones.AddRange(drones);
                context.SaveChanges();
        }
    }
}
