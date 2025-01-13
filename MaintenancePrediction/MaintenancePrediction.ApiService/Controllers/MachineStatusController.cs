﻿using MaintenancePrediction.ApiService.Services;
using MaintenancePrediction.ApiService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MaintenancePrediction.ApiService.Controllers
{
    // Handles endpoints for fetching machine status data.
    [ApiController]
    [Route("api/machine-status")]
    public class MachineStatusController : ControllerBase
    {
        private readonly IMachineUsageService _usageService;
        private readonly IMachineEventService _eventService;
        private readonly IMachineMaintenanceCheckResultService _maintenanceCheckResultService;

        public MachineStatusController(
            IMachineUsageService usageService, 
            IMachineEventService eventService,
            IMachineMaintenanceCheckResultService maintenanceCheckResultService)
        {
            _usageService = usageService;
            _eventService = eventService;
            _maintenanceCheckResultService = maintenanceCheckResultService;
        }

        [HttpPost("update-usage")]
        public async Task<IActionResult> UpdateUsage(int machineId, float hours, int cycles)
        {
            await _usageService.UpdateUsageAsync(machineId, hours, cycles);
            return Ok();
        }

        [HttpPost("log-event")]
        public async Task<IActionResult> LogEvent(int machineId, string eventCode, string description)
        {
            await _eventService.LogEventAsync(machineId, eventCode, description);
            return Ok();
        }

        [HttpGet("usage/{machineId}")]
        public async Task<IActionResult> GetUsage(int machineId)
        {
            var usage = await _usageService.GetUsageAsync(machineId);
            return Ok(usage);
        }

        [HttpGet("events/{machineId}")]
        public async Task<IActionResult> GetEvents(int machineId)
        {
            var events = await _eventService.GetEventsAsync(machineId);
            return Ok(events);
        }

        [HttpGet("maintenance-check/{machineId}")]
        public async Task<IActionResult> GetMaintenanceCheckResult(int machineId)
        {
            var results = await _maintenanceCheckResultService.GetMachineMaintenanceCheckResultAsync(machineId);
            return Ok(results);
        }
    }

}