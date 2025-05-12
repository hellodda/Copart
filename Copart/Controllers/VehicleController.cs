using Copart.BLL.Models.VehicleModels;
using Copart.BLL.Services.VehicleService;
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
            var result = await _service.GetAllAsync(token);
            return result.Success
                ? Ok(result.Data)
                : StatusCode(500, result.Message);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken token)
        {
            var result = await _service.GetByIdAsync(id, token);
            return result.Success
                ? Ok(result.Data)
                : NotFound(result.Message);
        }

        [HttpGet("make/{make}")]
        public async Task<IActionResult> GetByMake(string make, CancellationToken token)
        {
            var result = await _service.GetByMakeAsync(make, token);
            return result.Success
                ? Ok(result.Data)
                : NotFound(result.Message);
        }

        [HttpGet("model/{model}")]
        public async Task<IActionResult> GetByModel(string model, CancellationToken token)
        {
            var result = await _service.GetByModelAsync(model, token);
            return result.Success
                ? Ok(result.Data)
                : NotFound(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] VehicleAddModel vehicle, CancellationToken token)
        {
            var result = await _service.AddAsync(vehicle, token);
            return result.Success
                ? Ok(result.Message)
                : BadRequest(result.Message);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] VehicleUpdateModel vehicle, CancellationToken token)
        {
            var result = await _service.UpdateAsync(id, vehicle, token);
            return result.Success
                ? Ok(result.Message)
                : BadRequest(result.Message);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken token)
        {
            var result = await _service.DeleteAsync(id, token);
            return result.Success
                ? Ok(result.Message)
                : NotFound(result.Message);
        }
    }
}
