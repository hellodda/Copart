using Copart.Domain.Entities;

namespace Copart.Domain.BaseRepositories
{
    public interface IUserRepository
    {
        public Task AddAsync(User user, CancellationToken token = default);
        public Task<IEnumerable<User>> GetAllAsync(CancellationToken token = default);
        public Task<User?> GetByIdAsync(int id, CancellationToken token = default);
        public Task UpdateAsync(User user, CancellationToken token = default);
        public Task DeleteAsync(User user, CancellationToken token = default);
    }
}
