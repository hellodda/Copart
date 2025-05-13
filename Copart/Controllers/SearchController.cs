using Copart.BLL.Models.VehicleModels;
using Copart.BLL.Services.LotService;
using Copart.BLL.Services.SearchService;
using Copart.BLL.Services.VehicleService;
using Microsoft.AspNetCore.Mvc;

namespace Copart.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly ILogger<SearchController> _logger;

        public SearchController(ISearchService searchService, ILogger<SearchController> logger)
        {
            _logger = logger;
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var result = await _searchService.Search<VehicleModel, IVehicleService>(query);

            if (!result.Success)
            {
                _logger.LogWarning("Search Error: {Message}", result.Message);
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }
    }
}
