using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Copart.Data.Repositories
{
    public class LotRepository : ILotRepository
    {
        private readonly CopartDbContext _context;
        private readonly ILogger<LotRepository> _logger;

        public LotRepository(CopartDbContext context, ILogger<LotRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(Lot lot, CancellationToken token = default)
        {
           await _context.Lots.AddAsync(lot);
        }

        public Task DeleteAsync(Lot lot, CancellationToken token = default)
        {
            _context.Remove(lot);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Lot>> GetAllAsync(CancellationToken token = default)
        {
            return await _context.Lots.ToListAsync(token);
        }

        public async Task<Lot?> GetByIdAsync(int id, CancellationToken token = default)
        {
            return await _context.Lots.FirstOrDefaultAsync(l => l.Id == id, token);
        }

        public async Task<Lot?> GetByLotNumberAsync(string lotNumber, CancellationToken token = default)
        {
            return await _context.Lots.FirstOrDefaultAsync(l => l.LotNumber == lotNumber, token);
        }

        public Task UpdateAsync(Lot lot, CancellationToken token = default)
        {
            _context.Lots.Update(lot);
            return Task.CompletedTask;
        }
    }
}
