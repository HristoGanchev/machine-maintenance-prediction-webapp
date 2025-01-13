using MaintenancePrediction.ApiService.Data.Models;
using MaintenancePrediction.ApiService.Data;
using Microsoft.EntityFrameworkCore;

namespace MaintenancePrediction.ApiService.Services
{
    // Update and fetch machine usage data in a service.
    public class MachineUsageService
    {
        private readonly MachineMaintenanceDbContext _context;

        public MachineUsageService(MachineMaintenanceDbContext context)
        {
            _context = context;
        }

        public async Task UpdateUsageAsync(int machineId, float additionalHours, int additionalCycles)
        {
            var usage = await _context.MachineUsages.FirstOrDefaultAsync(u => u.MachineId == machineId);
            if (usage == null)
            {
                usage = new MachineUsage
                {
                    MachineId = machineId,
                    RuntimeHours = additionalHours,
                    CycleCount = additionalCycles,
                    LastUpdated = DateTime.UtcNow
                };
                await _context.MachineUsages.AddAsync(usage);
            }
            else
            {
                usage.RuntimeHours += additionalHours;
                usage.CycleCount += additionalCycles;
                usage.LastUpdated = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<MachineUsage> GetUsageAsync(int machineId)
        {
            return await _context.MachineUsages.FirstOrDefaultAsync(u => u.MachineId == machineId);
        }
    }
}
