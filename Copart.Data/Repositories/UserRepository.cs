using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Copart.Data.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly CopartDbContext _context;

        public UserRepository(CopartDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user, CancellationToken token = default)
        {
            await _context.Users.AddAsync(user, token).ConfigureAwait(false);
        }

        public Task AddBidAsync(User user, Bid bid, CancellationToken token = default)
        {
            user.Bids.Add(bid);
            _context.Users.Update(user);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(User user, CancellationToken token = default)
        {
            _context.Users.Remove(user);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<User?>?> GetAllAsync(CancellationToken token = default)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Bids)
                .ToListAsync(token)
                .ConfigureAwait(false);
        }

        public async Task<User?> GetByIdAsync(int id, CancellationToken token = default)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Bids)
                .FirstOrDefaultAsync(u => u.Id == id, token)
                .ConfigureAwait(false);
        }

        public async Task<User?> GetByNameAsync(string name, CancellationToken token = default)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Bids)
                .FirstOrDefaultAsync(u => u.Name == name, token)
                .ConfigureAwait(false);
        }

        public Task UpdateAsync(User user, CancellationToken token = default)
        {
            _context.Users.Update(user);
            return Task.CompletedTask;
        }
    }
}