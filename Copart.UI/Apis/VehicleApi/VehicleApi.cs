using Copart.UI.Models.VehicleModels;
using System.Net.Http.Json;

namespace Copart.UI.Apis.VehicleApi
{
    public class VehicleApi : IVehicleApi
    {
        private readonly HttpClient _client;

        public VehicleApi(HttpClient client)
        {
            _client = client;
        }

        public Task<IEnumerable<VehicleModel>> GetAll()
        {
            try
            {
                return _client.GetFromJsonAsync<IEnumerable<VehicleModel>>("/Vehicle");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
