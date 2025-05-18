using Copart.BLL.Models.BidModels;
using Copart.BLL.Models.UserModels;
using Copart.BLL.Services.BidderService;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Copart.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            _logger.LogDebug("GET /api/User called");
            var result = await _userService.GetAllAsync(token);
            if (!result.Success)
            {
                _logger.LogWarning("GetAll failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name, CancellationToken token)
        {
            _logger.LogDebug("GET /api/User/name called with {Name}", name);
            var result = await _userService.GetByNameAsync(name, token);
            if (!result.Success)
            {
                _logger.LogWarning("GetByName failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserAddModel user, [FromServices] IValidator<UserAddModel> validator, CancellationToken token)
        {
            _logger.LogDebug("POST /api/User called with {@User}", user);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Add ModelState invalid: {@Errors}", ModelState);
                return BadRequest(ModelState);
            }

            var result = await _userService.AddAsync(user, validator, token);
            if (!result.Success)
            {
                _logger.LogWarning("Add failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }

            return Created(string.Empty, result.Message);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserUpdateModel user, CancellationToken token)
        {
            _logger.LogDebug("PUT /api/User/{Id} called with {@User}", id, user);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Update ModelState invalid: {@Errors}", ModelState);
                return BadRequest(ModelState);
            }

            var result = await _userService.UpdateAsync(id, user, token);
            if (!result.Success)
            {
                _logger.LogWarning("Update failed for Id={Id}: {Message}", id, result.Message);
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken token)
        {
            _logger.LogDebug("DELETE /api/User/{Id} called", id);
            var result = await _userService.DeleteAsync(id, token);
            if (!result.Success)
            {
                _logger.LogWarning("Delete failed for Id={Id}: {Message}", id, result.Message);
                return NotFound(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpPost("{id:int}/bids")]
        public async Task<IActionResult> AddBid([FromRoute] int id, [FromBody] BidAddModel bid, CancellationToken token)
        {
            _logger.LogDebug("PATCH /api/User/{Id}/bids called with {@Bid}", id, bid);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("AddBid ModelState invalid: {@Errors}", ModelState);
                return BadRequest(ModelState);
            }

            var result = await _userService.AddBidAsync(id, bid, token);
            if (!result.Success)
            {
                _logger.LogWarning("AddBid failed for Id={Id}: {Message}", id, result.Message);
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
    }
}
