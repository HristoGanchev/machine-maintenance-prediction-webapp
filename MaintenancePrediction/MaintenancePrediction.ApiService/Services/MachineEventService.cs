﻿using MaintenancePrediction.ApiService.Data;
using Microsoft.EntityFrameworkCore;
using MaintenancePrediction.ApiService.Models;
using MaintenancePrediction.ApiService.Services.Interfaces;

namespace MaintenancePrediction.ApiService.Services
{
    public class MachineEventService : IMachineEventService
    {
        private readonly MachineMaintenanceDbContext _context;

        public MachineEventService(MachineMaintenanceDbContext context)
        {
            _context = context;
        }

        public async Task LogEventAsync(int machineId, string eventCode, string description)
        {
            var machineEvent = new MachineEvent
            {
                MachineId = machineId,
                EventCode = eventCode,
                Description = description,
                Timestamp = DateTime.UtcNow
            };

            await _context.MachineEvents.AddAsync(machineEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MachineEvent>> GetEventsAsync(int machineId)
        {
            var responce = await _context.MachineEvents
                .Where(e => e.MachineId == machineId)
                .ToListAsync();
            if (responce.Count() == 0)
            {
                return new List<MachineEvent>();
            }
            return responce;
        }
    }
}
