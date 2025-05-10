using Copart.BLL.Models.LotModels;
using Copart.BLL.Services.LotService;
using Microsoft.AspNetCore.Mvc;

namespace Copart.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LotController : ControllerBase
    {
        private readonly ILotService _service;
        private readonly ILogger<LotController> _logger;

        public LotController(ILotService service, ILogger<LotController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var result = await _service.GetAll(token);
            if (!result.Success) BadRequest(result.Message);
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken token)
        {
            var result = await _service.GetById(id, token);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Data);
        }

        [HttpGet("{lotNumber:alpha}")]
        public async Task<IActionResult> GetByLotNumber([FromRoute] string lotNumber, CancellationToken token)
        {
            var result = await _service.GetByLotNumber(lotNumber, token);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LotAddModel lot, CancellationToken token)
        {
            var result = await _service.Add(lot, token);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] LotUpdateModel lot, [FromRoute] int id, CancellationToken token)
        {
            var result = await _service.Update(id, lot, token);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken token)
        {
            var result = await _service.Delete(id, token);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Message);
        }
    }
}
