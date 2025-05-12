using Copart.Domain.Entities;

namespace Copart.Domain.BaseRepositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User?>?> GetAllAsync(CancellationToken token = default);
        public Task<User?> GetByIdAsync(int id, CancellationToken token = default);
        public Task AddAsync(User user, CancellationToken token = default);
        public Task AddBidAsync(User user, Bid bid, CancellationToken token = default);
        public Task UpdateAsync(User user, CancellationToken token = default);
        public Task DeleteAsync(User user, CancellationToken token = default);
    }
}
