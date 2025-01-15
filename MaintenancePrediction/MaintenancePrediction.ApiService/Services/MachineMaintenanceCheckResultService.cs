using MaintenancePrediction.ApiService.Data;
using Microsoft.EntityFrameworkCore;
using MaintenancePrediction.ApiService.Models;
using MaintenancePrediction.ApiService.Services.Interfaces;

namespace MaintenancePrediction.ApiService.Services
{
    // Mixin class for gathering machine maintenance check results
    public class MachineMaintenanceCheckResultService : IMachineMaintenanceCheckResultService
    {
        private readonly MachineMaintenanceDbContext _context;

        public MachineMaintenanceCheckResultService(MachineMaintenanceDbContext context)
        {
            _context = context;
        }

        public async Task<MachineMaintenanceCheckResult> GetMachineMaintenanceCheckResultAsync(int machineId)
        {
            // Get the latest machine maintenance check
            var responce = await _context.MachineMaintenanceChecks
                .FirstOrDefaultAsync(e => e.MachineId == machineId);

            if (responce == null)
            {
                return new MachineMaintenanceCheckResult();
            }
            return responce;
        }

        public async Task<MachineMaintenanceCheckResult> GetMachineMaintenanceCheckResultMixinAsync(int machineId)
        {
            // Get the machine
            var machine = await _context.Machines.FindAsync(machineId);

            // Get the latest machine events
            var events = await _context.MachineEvents
                .Where(e => e.MachineId == machineId)
                .OrderByDescending(e => e.Timestamp)
                .ToListAsync();

            // Get the latest machine usage
            var usage = await _context.MachineUsages
                .Where(u => u.MachineId == machineId)
                .OrderByDescending(u => u.LastUpdated)
                .FirstOrDefaultAsync();

            // Get the latest machine maintenance check
            var responce = await _context.MachineMaintenanceChecks
                .FirstOrDefaultAsync(e => e.MachineId == machineId);

            // Update the machine maintenance check
            if (responce == null)
            {
                responce = new MachineMaintenanceCheckResult();
                _context.MachineMaintenanceChecks.Add(responce);
            }
            else
            {
                responce.MachineId = machineId;
                responce.RuntimeHours = usage.RuntimeHours;
                responce.CycleCount = usage.CycleCount;
                responce.CycleThreshold = machine.CycleCountThreshold;
                responce.RuntimeThreshold = machine.RuntimeThreshold;

                responce.RequiresMaintenance = CheckMaintenanceRequired(responce);

                _context.MachineMaintenanceChecks.Update(responce);
            }

            return responce;
        }

        private bool CheckMaintenanceRequired(MachineMaintenanceCheckResult results)
        {
            return results.RuntimeHours > results.RuntimeThreshold 
                || results.CycleCount > results.CycleThreshold;
        }
    }
}
