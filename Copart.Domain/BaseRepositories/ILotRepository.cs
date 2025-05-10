using Copart.Domain.Entities;

namespace Copart.Domain.BaseRepositories
{
    public interface ILotRepository
    {
        public Task AddAsync(Lot lot, CancellationToken token = default);
        public Task<IEnumerable<Lot>> GetAllAsync(CancellationToken token = default);
        public Task<Lot?> GetByIdAsync(int id, CancellationToken token = default);
        public Task<Lot?> GetByLotNumberAsync(string lotNumber, CancellationToken token = default);
        public Task UpdateAsync(Lot lot, CancellationToken token = default);
        public Task DeleteAsync(Lot lot, CancellationToken token = default);
    }
}
