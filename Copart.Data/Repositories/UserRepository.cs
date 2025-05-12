using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Copart.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CopartDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(CopartDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(User user, CancellationToken token = default)
        {
            _logger.LogInformation("Adding new bidder: {@Bidder}", user);
            try
            {
                await _context.Users.AddAsync(user, token);
                _logger.LogInformation("Bidder added successfully: Id={BidderId}", user.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding bidder: {@Bidder}", user);
                throw;
            }
        }

        public Task DeleteAsync(User user, CancellationToken token = default)
        {
            _logger.LogInformation("Removing bidder: Id={BidderId}", user.Id);
            try
            {
                _context.Users.Remove(user);
                _logger.LogInformation("Bidder removal scheduled: Id={BidderId}", user.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing bidder: Id={BidderId}", user.Id);
                throw;
            }

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken token = default)
        {
            _logger.LogInformation("Retrieving all bidders from database");
            try
            {
                var list = await _context.Users.ToListAsync(token);
                _logger.LogInformation("Found {Count} bidders", list.Count);
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving list of bidders");
                throw;
            }
        }

        public async Task<User?> GetByIdAsync(int id, CancellationToken token = default)
        {
            _logger.LogInformation("Retrieving bidder by Id={BidderId}", id);
            try
            {
                var bidder = await _context.Users.FirstOrDefaultAsync(b => b.Id == id, token);
                if (bidder is null)
                    _logger.LogWarning("Bidder not found: Id={BidderId}", id);
                else
                    _logger.LogInformation("Bidder found: {@Bidder}", bidder);

                return bidder;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving bidder by Id={BidderId}", id);
                throw;
            }
        }

        public Task UpdateAsync(User user, CancellationToken token = default)
        {
            _logger.LogInformation("Updating bidder: {@Bidder}", user);
            try
            {
                _context.Users.Update(user);
                _logger.LogInformation("Bidder updated successfully: Id={BidderId}", user.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating bidder: {@Bidder}", user);
                throw;
            }

            return Task.CompletedTask;
        }
    }
}
