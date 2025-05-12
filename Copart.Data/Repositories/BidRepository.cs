using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Copart.Data.Repositories
{
    public sealed class BidRepository : IBidRepository
    {
        private readonly CopartDbContext _context;

        public BidRepository(CopartDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Bid bid, CancellationToken token = default)
        {
            await _context.Bids.AddAsync(bid, token).ConfigureAwait(false);
        }

        public Task DeleteAsync(Bid bid, CancellationToken token = default)
        {
            _context.Bids.Remove(bid);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Bid?>?> GetAllAsync(CancellationToken token = default)
        {
            return await _context.Bids
                .AsNoTracking()
                .Include(b => b.User)
                .Include(b => b.Lot)
                    .ThenInclude(l => l.Vehicle)
                .ToListAsync(token)
                .ConfigureAwait(false);
        }

        public async Task<Bid?> GetByIdAsync(int id, CancellationToken token = default)
        {
            return await _context.Bids
                .AsNoTracking()
                .Include(b => b.User)
                .Include(b => b.Lot)
                    .ThenInclude(l => l.Vehicle)
                .FirstOrDefaultAsync(b => b.Id == id, token)
                .ConfigureAwait(false);
        }

        public Task UpdateAsync(Bid bid, CancellationToken token = default)
        {
            _context.Bids.Update(bid);
            return Task.CompletedTask;
        }
    }
}