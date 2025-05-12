using Copart.BLL.Models.BidModels;
using Copart.BLL.Services.BidService;
using Microsoft.AspNetCore.Mvc;

namespace Copart.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;

        public BidController(IBidService bidService)
        {
            _bidService = bidService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var result = await _bidService.GetAll(token);
            if (result.Success) return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BidAddModel bid, CancellationToken token)
        {
            var result = await _bidService.Add(bid, token);
            if (result.Success) return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BidUpdateModel bid, CancellationToken token)
        {
            var result = await _bidService.Update(id, bid, token);
            if (result.Success) return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken token)
        {
            var result = await _bidService.Delete(id, token);
            if (result.Success) return Ok(result.Message);
            return BadRequest(result.Message);
        }
    }
}
