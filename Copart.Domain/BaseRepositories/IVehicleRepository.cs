using Copart.Domain.Entities;

namespace Copart.Domain.BaseRepositories
{
    public interface IVehicleRepository
    {
        public Task<IEnumerable<Vehicle?>?> GetAllAsync(CancellationToken token = default);
        public Task<IEnumerable<Vehicle?>?> GetByMakeAsync(string make, CancellationToken token = default);
        public Task<IEnumerable<Vehicle?>?> GetByModelAsync(string model, CancellationToken token = default);
        public Task<Vehicle?> GetByIdAsync(int id, CancellationToken token = default);
        public Task<Vehicle?> GetByVinAsync(string vin, CancellationToken token = default);
        public Task AddAsync(Vehicle vehicle, CancellationToken token = default);
        public Task UpdateAsync(Vehicle vehicle);
        public Task DeleteAsync(Vehicle vehicle);
    }
}
