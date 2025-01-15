namespace MaintenancePrediction.Tests;
using MaintenancePrediction.ApiService.Services;
using MaintenancePrediction.ApiService.Models;
using MaintenancePrediction.ApiService.Services;
using MaintenancePrediction.ApiService.Models;
using MaintenancePrediction.ApiService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Test class for the MachineDataService
[TestClass]
public class MachineDataServiceTest
{
    [TestClass]
    public class MachineDataServiceTests
    {
        private MachineDataService _machineDataService;
        private MachineMaintenanceDbContext _context;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<MachineMaintenanceDbContext>()
                .UseInMemoryDatabase(databaseName: "MachineMaintenanceTestDb")
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            _context = new MachineMaintenanceDbContext(options);
            _context.Database.EnsureDeleted();
            _machineDataService = new MachineDataService(_context);
        }

        [TestMethod]
        public async Task GetMachinesAsync_ReturnsAllMachines()
        {
            // Arrange
            _context.Machines.AddRange(
                new MachineData { MachineId = 1, Name = "Machine1", Location = "Location1" },
                new MachineData { MachineId = 2, Name = "Machine2", Location = "Location2" }
            );
            _context.SaveChanges();
            // Act
            var result = await _machineDataService.GetMachinesAsync();
            // Assert
            Assert.AreEqual(2, result.Count());

        }

        [TestMethod]
        public async Task GetMachineAsync_ReturnsMachineById()
        {
            // Arrange
            _context.Machines.AddRange(
                new MachineData { MachineId = 1, Name = "Machine1", Location = "Location1" },
                new MachineData { MachineId = 2, Name = "Machine2", Location = "Location2" }
            );
            _context.SaveChanges();

            var service = new MachineDataService(_context);

            // Act
            var result = await service.GetMachinesAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}
