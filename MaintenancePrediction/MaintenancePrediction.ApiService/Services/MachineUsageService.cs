using MaintenancePrediction.ApiService.Data;
using Microsoft.EntityFrameworkCore;
using MaintenancePrediction.ApiService.Models;
using MaintenancePrediction.ApiService.Services.Interfaces;

namespace MaintenancePrediction.ApiService.Services
{
    // Update and fetch machine usage data in a service.
    public class MachineUsageService : IMachineUsageService
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
            var responce = await _context.MachineUsages.FirstOrDefaultAsync(u => u.MachineId == machineId);
            if (responce == null)
            {
                return new MachineUsage();
            }
            return responce;
        }
    }
}
