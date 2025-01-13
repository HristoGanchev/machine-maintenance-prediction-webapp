using MaintenancePrediction.ApiService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaintenancePrediction.ApiService.Controllers
{
    // Handles endpoints for fetching machine status data.
    [ApiController]
    [Route("api/machine-status")]
    public class MachineStatusController : ControllerBase
    {
        private readonly MachineUsageService _usageService;
        private readonly MachineEventService _eventService;

        public MachineStatusController(MachineUsageService usageService, MachineEventService eventService)
        {
            _usageService = usageService;
            _eventService = eventService;
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
    }

}
