using Copart.BLL.Models.LotModels;
using System.Net.Http.Json;

namespace Copart.UI.Apis.LotApi
{
    public class LotApi : ILotApi
    {
        private readonly HttpClient _client;

        public LotApi(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<LotModel>> GetAll(CancellationToken token)
        {
            try
            {
                return await _client.GetFromJsonAsync<IEnumerable<LotModel>>("/Lot", token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching lots: {ex.Message}");
                throw;
            }
        }
    }
}
