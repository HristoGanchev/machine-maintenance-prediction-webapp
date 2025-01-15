using MaintenancePrediction.ApiService.Data;
using Microsoft.EntityFrameworkCore;
using MaintenancePrediction.ApiService.Models;
using MaintenancePrediction.ApiService.Services.Interfaces;
using System.Reflection.PortableExecutable;

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

        public async Task RunMachinesMaintenanceCheckResultMixinAsync()
        {
            // Get the machines
            var machines = await _context.Machines.ToListAsync();

            foreach (var machine in machines)
            {
                await RunMachinesMaintenanceCheckResultMixinAsync(machine);
            }    
        }

        private async Task RunMachinesMaintenanceCheckResultMixinAsync(MachineData machine)
        {
            // Get the latest machine events
            var events = await _context.MachineEvents
                .Where(e => e.MachineId == machine.MachineId)
                .OrderByDescending(e => e.Timestamp)
                .ToListAsync();

            // Get the latest machine usage
            var usage = await _context.MachineUsages
                .Where(u => u.MachineId == machine.MachineId)
                .OrderByDescending(u => u.LastUpdated)
                .FirstOrDefaultAsync();

            // Get the latest machine maintenance check
            var responce = await _context.MachineMaintenanceChecks
                .FirstOrDefaultAsync(e => e.MachineId == machine.MachineId);

            // Update the machine maintenance check
            if (responce == null)
            {
                responce = new MachineMaintenanceCheckResult();
                _context.MachineMaintenanceChecks.Add(responce);
            }
            else
            {
                responce.MachineId = machine.MachineId;
                responce.RuntimeHours = usage.RuntimeHours;
                responce.CycleCount = usage.CycleCount;
                responce.CycleThreshold = machine.CycleCountThreshold;
                responce.RuntimeThreshold = machine.RuntimeThreshold;

                responce.RequiresMaintenance = CheckMaintenanceRequired(responce);
                responce.Reason = GetMaintenanceReason(responce);

                _context.MachineMaintenanceChecks.Update(responce);
                await _context.SaveChangesAsync();
            }
        }

        private bool CheckMaintenanceRequired(MachineMaintenanceCheckResult results)
        {
            return results.RuntimeHours > results.RuntimeThreshold 
                || results.CycleCount > results.CycleThreshold;
        }

        private string GetMaintenanceReason(MachineMaintenanceCheckResult results)
        {
            if (results.RuntimeHours > results.RuntimeThreshold)
            {
                return "Runtime hours exceeded threshold";
            }
            else if (results.CycleCount > results.CycleThreshold)
            {
                return "Cycle count exceeded threshold";
            }
            return "No maintenance required";
        }
    }
}
