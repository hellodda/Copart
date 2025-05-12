using Copart.BLL.Models.VehicleModels;
using Copart.BLL.Results;

namespace Copart.BLL.Services.VehicleService
{
    public interface IVehicleService
    {
        public Task<Result<VehicleModel>> GetByIdAsync(int id, CancellationToken token = default);
        public Task<Result<IEnumerable<VehicleModel>>> GetAllAsync(CancellationToken token = default);
        public Task<Result<IEnumerable<VehicleModel>>> GetByMakeAsync(string make, CancellationToken token = default);
        public Task<Result<IEnumerable<VehicleModel>>> GetByModelAsync(string model, CancellationToken token = default);
        public Task<Result> AddAsync(VehicleAddModel vehicle, CancellationToken token = default);
        public Task<Result> UpdateAsync(int id, VehicleUpdateModel vehicle, CancellationToken token = default);
        public Task<Result> DeleteAsync(int id, CancellationToken token = default);
    }
}
