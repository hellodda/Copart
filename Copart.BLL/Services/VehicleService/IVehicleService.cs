using Copart.BLL.Models.VehicleModels;
using Copart.BLL.Results;

namespace Copart.BLL.Services.VehicleService
{
    public interface IVehicleService
    {
        public Task<Result<VehicleModel>> GetById(int id, CancellationToken token = default);
        public Task<Result<IEnumerable<VehicleModel>>> GetAll(CancellationToken token = default);
        public Task<Result<IEnumerable<VehicleModel>>> GetByMake(string make, CancellationToken token = default);
        public Task<Result<IEnumerable<VehicleModel>>> GetByModel(string model, CancellationToken token = default);
        public Task<Result> Add(VehicleAddModel vehicle, CancellationToken token = default);
        public Task<Result> Update(int id, VehicleUpdateModel vehicle, CancellationToken token = default);
        public Task<Result> Delete(int id, CancellationToken token = default);
    }
}
