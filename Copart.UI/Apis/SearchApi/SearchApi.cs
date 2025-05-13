using Copart.UI.Models.VehicleModels;
using System.Net.Http.Json;

namespace Copart.UI.Apis.SearchApi
{
    public class SearchApi : ISearchApi
    {
        private readonly HttpClient _client;

        public SearchApi(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<VehicleModel>> Search(string query)
        {
            return await _client.GetFromJsonAsync<IEnumerable<VehicleModel>>($"/Search/?query={query}");
        }

    }
}
