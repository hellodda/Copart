using Copart.BLL.Models.BidModels;
using Copart.BLL.Services.BidService;
using Microsoft.AspNetCore.Mvc;

namespace Copart.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;
        private readonly ILogger<BidController> _logger;

        public BidController(IBidService bidService, ILogger<BidController> logger)
        {
            _bidService = bidService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            _logger.LogDebug("GET /api/Bid called");
            var result = await _bidService.GetAllAsync(token);
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
            _logger.LogDebug("GET /api/Bid/{Id} called", id);
            var result = await _bidService.GetByIdAsync(id, token);
            if (!result.Success)
            {
                _logger.LogWarning("GetById failed for Id={Id}: {Message}", id, result.Message);
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BidAddModel model, CancellationToken token)
        {
            _logger.LogDebug("POST /api/Bid called with {@Model}", model);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState invalid for Add: {@Errors}", ModelState);
                return BadRequest(ModelState);
            }

            var result = await _bidService.AddAsync(model, token);
            if (!result.Success)
            {
                _logger.LogWarning("Add failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), result.Message);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BidUpdateModel model, CancellationToken token)
        {
            _logger.LogDebug("PUT /api/Bid/{Id} called with {@Model}", id, model);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState invalid for Update: {@Errors}", ModelState);
                return BadRequest(ModelState);
            }

            var result = await _bidService.UpdateAsync(id, model, token);
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
            _logger.LogDebug("DELETE /api/Bid/{Id} called", id);
            var result = await _bidService.DeleteAsync(id, token);
            if (!result.Success)
            {
                _logger.LogWarning("Delete failed for Id={Id}: {Message}", id, result.Message);
                return NotFound(result.Message);
            }
            return Ok(result.Message);
        }
    }
}
