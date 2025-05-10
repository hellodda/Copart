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
            _logger.LogDebug("AddAsync invoked with Lot entity: {@Lot}", lot);
            try
            {
                await _context.Lots.AddAsync(lot, token);
                _logger.LogInformation("Lot entity added to context (Id will be generated on Save)");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddAsync for Lot: {@Lot}", lot);
                throw;
            }
        }

        public Task DeleteAsync(Lot lot, CancellationToken token = default)
        {
            _logger.LogDebug("DeleteAsync invoked for Lot Id {Id}", lot.Id);
            try
            {
                _context.Lots.Remove(lot);
                _logger.LogInformation("Lot entity marked for deletion: Id {Id}", lot.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for Lot Id {Id}", lot.Id);
                throw;
            }

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Lot>> GetAllAsync(CancellationToken token = default)
        {
            _logger.LogDebug("GetAllAsync invoked");
            try
            {
                var list = await _context.Lots.ToListAsync(token);
                _logger.LogInformation("GetAllAsync retrieved {Count} lots", list.Count);
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync");
                throw;
            }
        }

        public async Task<Lot?> GetByIdAsync(int id, CancellationToken token = default)
        {
            _logger.LogDebug("GetByIdAsync invoked for Id {Id}", id);
            try
            {
                var lot = await _context.Lots.FirstOrDefaultAsync(l => l.Id == id, token);
                if (lot is null)
                    _logger.LogWarning("GetByIdAsync: Lot Id {Id} not found", id);
                else
                    _logger.LogInformation("GetByIdAsync: Lot Id {Id} retrieved", id);

                return lot;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync for Id {Id}", id);
                throw;
            }
        }

        public async Task<Lot?> GetByLotNumberAsync(string lotNumber, CancellationToken token = default)
        {
            _logger.LogDebug("GetByLotNumberAsync invoked for LotNumber '{LotNumber}'", lotNumber);
            try
            {
                var lot = await _context.Lots.FirstOrDefaultAsync(l => l.LotNumber == lotNumber, token);
                if (lot is null)
                    _logger.LogWarning("GetByLotNumberAsync: LotNumber '{LotNumber}' not found", lotNumber);
                else
                    _logger.LogInformation("GetByLotNumberAsync: LotNumber '{LotNumber}' retrieved", lotNumber);

                return lot;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByLotNumberAsync for LotNumber '{LotNumber}'", lotNumber);
                throw;
            }
        }

        public Task UpdateAsync(Lot lot, CancellationToken token = default)
        {
            _logger.LogDebug("UpdateAsync invoked for Lot Id {Id}: {@Lot}", lot.Id, lot);
            try
            {
                _context.Lots.Update(lot);
                _logger.LogInformation("Lot entity updated in context: Id {Id}", lot.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for Lot Id {Id}: {@Lot}", lot.Id, lot);
                throw;
            }

            return Task.CompletedTask;
        }
    }
}
