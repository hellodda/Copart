using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Copart.Data.Repositories
{
    public sealed class LotRepository : ILotRepository
    {
        private readonly CopartDbContext _context;

        public LotRepository(CopartDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Lot lot, CancellationToken token = default)
        {
            await _context.Lots.AddAsync(lot, token).ConfigureAwait(false);
        }

        public Task AddBidAsync(Lot lot, Bid bid, CancellationToken token = default)
        {
            lot.Bids.Add(bid);
            _context.Lots.Update(lot);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Lot lot, CancellationToken token = default)
        {
            _context.Lots.Remove(lot);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Lot?>?> GetAllAsync(CancellationToken token = default)
        {
            return await _context.Lots
                .AsNoTracking()
                .Include(l => l.Bids)
                .Include(l => l.Vehicle)
                .ToListAsync(token)
                .ConfigureAwait(false);
        }

        public async Task<Lot?> GetByIdAsync(int id, CancellationToken token = default)
        {
            return await _context.Lots
                .AsNoTracking()
                .Include(l => l.Vehicle)
                .FirstOrDefaultAsync(l => l.Id == id, token)
                .ConfigureAwait(false);
        }

        public async Task<Lot?> GetByLotNumberAsync(string lotNumber, CancellationToken token = default)
        {
            return await _context.Lots
                .AsNoTracking()
                .Include(l => l.Vehicle)
                .FirstOrDefaultAsync(l => l.LotNumber == lotNumber, token)
                .ConfigureAwait(false);
        }

        public Task UpdateAsync(Lot lot, CancellationToken token = default)
        {
            _context.Lots.Update(lot);
            return Task.CompletedTask;
        }
    }
}