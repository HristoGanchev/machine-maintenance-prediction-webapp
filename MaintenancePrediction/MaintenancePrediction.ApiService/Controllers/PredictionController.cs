namespace MaintenancePrediction.ApiService.Controllers
{
    using MaintenancePrediction.ApiService.Services;
    using MaintenancePrediction.ApiService.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    // Provides endpoints to predict maintenance events.
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionController : ControllerBase
    {
        private readonly IPredictionService _predictionService;

        public PredictionController(IPredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        [HttpGet("predict-maintenance/{machineId}")]
        public async Task<IActionResult> PredictMaintenance(int machineId)
        {
            var prediction = await _predictionService.PredictMaintenanceAsync(machineId);
            if (prediction == null) return NotFound("No prediction available for this machine.");

            return Ok(prediction);
        }

        [HttpGet("status")]
        public IActionResult GetPredictionStatus()
        {
            var status = _predictionService.GetLastPredictionStatus();
            return Ok(new { status });
        }
    }

}
