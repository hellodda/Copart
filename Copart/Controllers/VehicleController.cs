using Copart.BLL.Models;
using Copart.BLL.Services.VehicleService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            var result = await _service.GetAll(token);
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken token)
        {
            var result = await _service.GetById(id, token);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }

        [HttpGet("make/{make}")]
        public async Task<IActionResult> GetByMake(string make, CancellationToken token)
        {
            var result = await _service.GetByMake(make, token);
            return Ok(result.Data);
        }

        [HttpGet("model/{model}")]
        public async Task<IActionResult> GetByModel(string model, CancellationToken token)
        {
            var result = await _service.GetByModel(model, token);
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] VehicleAddModel vehicle, CancellationToken token)
        {
            var result = await _service.Add(vehicle, token);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, CancellationToken token)
        {
            var result = await _service.Update(id, token);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken token)
        {
            var result = await _service.Delete(id, token);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return NotFound(result.Message);
        }
    }
}
