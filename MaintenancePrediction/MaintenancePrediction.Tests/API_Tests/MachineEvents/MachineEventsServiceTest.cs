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

[TestClass]
public class MachineEventsServiceTest
{
    private MachineEventService _machineEventService;
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
        _machineEventService = new MachineEventService(_context);
    }

    [TestMethod]
    public async Task GetEventsAsync_ReturnsEvent()
    {
        // Arrange
        _context.MachineEvents.AddRange(
            new MachineEvent { EventId = 1, MachineId = 1, EventCode = "E101", Description = "descr 1" },
            new MachineEvent { EventId = 2, MachineId = 2, EventCode = "E102", Description = "descr 2" }
        );
        _context.SaveChanges();

        // Act
        var result = await _machineEventService.GetEventsAsync(machineId: 1);

        // Assert
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual("E101", result.First().EventCode);
    }
}
