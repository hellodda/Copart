using Copart.BLL.Models.BidModels;
using Copart.BLL.Models.LotModels;
using Copart.BLL.Services.LotService;
using FluentValidation;
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
            _logger.LogDebug("GET /api/Lot called");
            var result = await _service.GetAllAsync(token);
            if (!result.Success)
            {
                _logger.LogWarning("GetAll failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken token)
        {
            _logger.LogDebug("GET /api/Lot/{Id} called", id);
            var result = await _service.GetByIdAsync(id, token);
            if (!result.Success)
            {
                _logger.LogWarning("GetById failed for Id={Id}: {Message}", id, result.Message);
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpGet("by-number/{lotNumber}")]
        public async Task<IActionResult> GetByLotNumber(string lotNumber, CancellationToken token)
        {
            _logger.LogDebug("GET /api/Lot/by-number/{LotNumber} called", lotNumber);
            var result = await _service.GetByLotNumberAsync(lotNumber, token);
            if (!result.Success)
            {
                _logger.LogWarning("GetByLotNumber failed for Number={LotNumber}: {Message}", lotNumber, result.Message);
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LotAddModel model, [FromServices] IValidator<LotAddModel> validator, CancellationToken token)
        {
            _logger.LogDebug("POST /api/Lot called with {@Model}", model);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Add ModelState invalid: {@Errors}", ModelState);
                return BadRequest(ModelState);
            }

            var result = await _service.AddAsync(model, validator, token);
            if (!result.Success)
            {
                _logger.LogWarning("Add failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), result.Message);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] LotUpdateModel model, CancellationToken token)
        {
            _logger.LogDebug("PUT /api/Lot/{Id} called with {@Model}", id, model);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Update ModelState invalid: {@Errors}", ModelState);
                return BadRequest(ModelState);
            }

            var result = await _service.UpdateAsync(id, model, token);
            if (!result.Success)
            {
                _logger.LogWarning("Update failed for Id={Id}: {Message}", id, result.Message);
                return result.Message!.Contains("not found")
                    ? NotFound(result.Message)
                    : BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken token)
        {
            _logger.LogDebug("DELETE /api/Lot/{Id} called", id);
            var result = await _service.DeleteAsync(id, token);
            if (!result.Success)
            {
                _logger.LogWarning("Delete failed for Id={Id}: {Message}", id, result.Message);
                return NotFound(result.Message);
            }
            return Ok(result.Message);
        }

        [HttpGet("{id:int}/biggest")]
        public async Task<IActionResult> GetBiggestBid([FromRoute] int id, CancellationToken token)
        {
            _logger.LogDebug("GET /api/Lot/{Id}/biggest called", id);
            var result = await _service.GetBiggestBidAsync(id, token);
            if (!result.Success)
            {
                _logger.LogWarning("GetBiggestBid failed for Id={Id}: {Message}", id, result.Message);
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpPost("{id:int}/bids")]
        public async Task<IActionResult> AddBid([FromRoute] int id, [FromBody] BidAddModel bid, CancellationToken token)
        {
            _logger.LogDebug("PATCH /api/Lot/{Id}/bids called with {@Bid}", id, bid);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("AddBid ModelState invalid: {@Errors}", ModelState);
                return BadRequest(ModelState);
            }

            var result = await _service.AddBidAsync(id, bid, token);
            if (!result.Success)
            {
                _logger.LogWarning("AddBid failed for Id={Id}: {Message}", id, result.Message);
                return result.Message!.Contains("not found")
                    ? NotFound(result.Message)
                    : BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
    }
}
