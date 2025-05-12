using Copart.BLL.Models.BidModels;
using Copart.BLL.Models.UserModels;
using Copart.BLL.Services.BidderService;
using Microsoft.AspNetCore.Mvc;

namespace Copart.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var result = await userService.GetAll(token);
            if (result.Success) return Ok(result.Data);
            return BadRequest(result.Message);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserAddModel user, CancellationToken token)
        {
            var result = await userService.Add(user, token);
            if (result.Success) return Ok(result.Message);
            return BadRequest(result.Message);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserUpdateModel user, CancellationToken token)
        {
            var result = await userService.Update(id, user, token);
            if (result.Success) return Ok(result.Message);
            return BadRequest(result.Message);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken token)
        {
            var result = await userService.Delete(id, token);
            if (result.Success) return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> AddBid([FromRoute] int id, BidAddModel bid, CancellationToken token)
        {
            var result = await userService.AddBid(id, bid, token);
            if (result.Success) return Ok(result.Message);
            return BadRequest(result.Message);
        }
    }
}
