using Copart.BLL.Models.VehicleModels;
using Copart.BLL.Services.VehicleService;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Copart.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _service;
        private readonly ILogger<VehicleController> _logger;

        public VehicleController(IVehicleService service, ILogger<VehicleController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            _logger.LogDebug("Fetching all vehicles");
            var result = await _service.GetAllAsync(token);
            if (result.Success)
            {
                _logger.LogInformation("Successfully fetched {Count} vehicles", result.Data?.Count());
                return Ok(result.Data);
            }

            _logger.LogError("Failed to fetch vehicles: {Message}", result.Message);
            return StatusCode(500, result.Message);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken token)
        {
            _logger.LogDebug("Fetching vehicle by ID: {Id}", id);
            var result = await _service.GetByIdAsync(id, token);
            if (result.Success)
            {
                _logger.LogInformation("Vehicle with ID {Id} found", id);
                return Ok(result.Data);
            }

            _logger.LogWarning("Vehicle with ID {Id} not found: {Message}", id, result.Message);
            return NotFound(result.Message);
        }

        [HttpGet("make/{make}")]
        public async Task<IActionResult> GetByMake(string make, CancellationToken token)
        {
            _logger.LogDebug("Fetching vehicles by make: {Make}", make);
            var result = await _service.GetByMakeAsync(make, token);
            if (result.Success)
            {
                _logger.LogInformation("Found vehicles with make {Make}", make);
                return Ok(result.Data);
            }

            _logger.LogWarning("No vehicles found for make {Make}: {Message}", make, result.Message);
            return NotFound(result.Message);
        }

        [HttpGet("model/{model}")]
        public async Task<IActionResult> GetByModel(string model, CancellationToken token)
        {
            _logger.LogDebug("Fetching vehicles by model: {Model}", model);
            var result = await _service.GetByModelAsync(model, token);
            if (result.Success)
            {
                _logger.LogInformation("Found vehicles with model {Model}", model);
                return Ok(result.Data);
            }

            _logger.LogWarning("No vehicles found for model {Model}: {Message}", model, result.Message);
            return NotFound(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] VehicleAddModel vehicle, [FromServices] IValidator<VehicleAddModel> validator, CancellationToken token)
        {
            _logger.LogDebug("Adding vehicle: {@Vehicle}", vehicle);
            var result = await _service.AddAsync(vehicle, validator, token);
            if (result.Success)
            {
                _logger.LogInformation("Vehicle added successfully: {Message}", result.Message);
                return Ok(result.Message);
            }

            _logger.LogError("Failed to add vehicle: {Message}", result.Message);
            return BadRequest(result.Message);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] VehicleUpdateModel vehicle, CancellationToken token)
        {
            _logger.LogDebug("Updating vehicle ID {Id} with data: {@Vehicle}", id, vehicle);
            var result = await _service.UpdateAsync(id, vehicle, token);
            if (result.Success)
            {
                _logger.LogInformation("Vehicle ID {Id} updated successfully", id);
                return Ok(result.Message);
            }

            _logger.LogError("Failed to update vehicle ID {Id}: {Message}", id, result.Message);
            return BadRequest(result.Message);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken token)
        {
            _logger.LogDebug("Deleting vehicle with ID: {Id}", id);
            var result = await _service.DeleteAsync(id, token);
            if (result.Success)
            {
                _logger.LogInformation("Vehicle ID {Id} deleted", id);
                return Ok(result.Message);
            }

            _logger.LogWarning("Failed to delete vehicle ID {Id}: {Message}", id, result.Message);
            return NotFound(result.Message);
        }
    }
}
