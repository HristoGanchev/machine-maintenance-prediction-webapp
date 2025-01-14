using MaintenancePrediction.ApiService.Services.Interfaces;
using MaintenancePrediction.ApiService.Models;
using MaintenancePrediction.ApiService.Data;
using Microsoft.EntityFrameworkCore;

namespace MaintenancePrediction.ApiService.Services
{
    // Fetches and preprocesses data from the database.
    public class MachineDataService: IMachineDataService
    {
        private readonly MachineMaintenanceDbContext _context;

        public MachineDataService(MachineMaintenanceDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<MachineData>> GetMachinesAsync()
        {
            // Add logic to fetch machines from the database
            return await _context.Machines.ToListAsync();
        }
    }
}
