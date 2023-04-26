using Drones.Controllers;
using Drones.Infrastructure.DataContext;
using DronesTests.Resources.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesTests.Infrastructure
{
    public class BaseContextTest : IDisposable
    {
        protected readonly AppDBContext _context;
        /// <summary>
        /// Used to define service.
        /// </summary>
        public ServiceCollection services;
        /// <summary>
        /// IOC to resolve dependencies. Use NOT IServiceProvider to weird casting.
        /// </summary>
        /// 
        public ServiceProvider Container =>
            services.BuildServiceProvider();
        public BaseContextTest()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            this._context= new AppDBContext(options);
            this._context.Database.EnsureCreated();
            services = new ServiceCollection(); 
            services.AddScoped<ILogger<DispatchController>, SpyLogger<DispatchController>>();
            services.AddScoped<DispatchController>();


            DbInitializerTest.Initialize(this._context,services);
            services.AddTransient<AppDBContext>(c => this._context);



        }
        public void Dispose()
        {
            this._context.Database.EnsureCreated();
            this._context.Dispose();
        }
    }
}
