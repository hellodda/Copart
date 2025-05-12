using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Copart.Data.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly CopartDbContext _context;
        private readonly ILogger<BidRepository> _logger;

        public BidRepository(CopartDbContext context, ILogger<BidRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(Bid bid, CancellationToken token = default)
        {
            await _context.Bids.AddAsync(bid, token);
        }

        public Task DeleteAsync(Bid bid, CancellationToken token = default)
        {
            _context.Bids.Remove(bid);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Bid>> GetAllAsync(CancellationToken token = default)
        {
            return await _context.Bids.ToListAsync(token);
        }

        public async Task<Bid> GetByIdAsync(int id, CancellationToken token = default)
        {
            return await _context.Bids.FirstOrDefaultAsync(b => b.Id == id, token);
        }

        public Task UpdateAsync(Bid bid, CancellationToken token = default)
        {
            _context.Bids.Update(bid);
            return Task.CompletedTask;
        }
    }
}
