using Copart.UI.Models.VehicleModels;

namespace Copart.UI.Apis.VehicleApi
{
    public interface IVehicleApi
    {
        public Task<IEnumerable<VehicleModel>> GetAll();
    }
}
