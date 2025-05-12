using Copart.Domain.Entities;

namespace Copart.Domain.BaseRepositories
{
    public interface IBidderRepository
    {
        public Task AddAsync(Bidder bidder, CancellationToken token = default);
        public Task<IEnumerable<Bidder>> GetAllAsync(CancellationToken token = default);
        public Task<Bidder?> GetByIdAsync(int id, CancellationToken token = default);
        public Task UpdateAsync(Bidder bidder, CancellationToken token = default);
        public Task DeleteAsync(Bidder bidder, CancellationToken token = default);
    }
}
