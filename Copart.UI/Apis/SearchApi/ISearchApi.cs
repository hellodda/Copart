using Copart.UI.Models.VehicleModels;

namespace Copart.UI.Apis.SearchApi
{
    public interface ISearchApi
    {
        public Task<IEnumerable<VehicleModel>> Search(string query);
    }
}
