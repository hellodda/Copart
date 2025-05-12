using Copart.Domain.Entities;

namespace Copart.Domain.BaseRepositories
{
    public interface IBidRepository
    {
        public Task<Bid> GetByIdAsync(int id, CancellationToken token = default);
        public Task<IEnumerable<Bid>> GetAllAsync(CancellationToken token = default);
        public Task AddAsync(Bid bid, CancellationToken token = default);
        public Task UpdateAsync(Bid bid, CancellationToken token = default);
        public Task DeleteAsync(Bid bid, CancellationToken token = default);
    }
}
