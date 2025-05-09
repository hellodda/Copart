using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Copart.Data.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly CopartDbContext _context;
        private readonly ILogger<VehicleRepository> _logger;

        public VehicleRepository(CopartDbContext context, ILogger<VehicleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IQueryable<Vehicle> Query()
        {
            return _context.Vehicles.AsNoTracking();
        }

        public async Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Vehicles.AddAsync(vehicle, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add Vehicle {@Vehicle}", vehicle);
            }
        }

        public async Task<Vehicle?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.Vehicles
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve Vehicle with Id={VehicleId}", id);
                return null;
            }
        }

        public async Task<IEnumerable<Vehicle>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.Vehicles
                                     .AsNoTracking()
                                     .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve all Vehicles");
                return null;
            }
        }

        public async Task<IEnumerable<Vehicle>?> GetByMakeAsync(string make, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.Vehicles
                                     .AsNoTracking()
                                     .Where(v => v.Make == make)
                                     .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve Vehicles by Make={Make}", make);
                return null;
            }
        }

        public async Task<IEnumerable<Vehicle>?> GetByModelAsync(string model, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.Vehicles
                                     .AsNoTracking()
                                     .Where(v => v.Model == model)
                                     .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve Vehicles by Model={Model}", model);
                return null;
            }
        }

        public async Task<Vehicle?> GetByVinAsync(string vin, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.Vehicles
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(v => v.Vin == vin, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve Vehicle by VIN={Vin}", vin);
                return null;
            }
        }

        public void Update(Vehicle vehicle)
        {
            try
            {
                _context.Vehicles.Update(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update Vehicle {@Vehicle}", vehicle);
            }
        }

        public void Delete(Vehicle vehicle)
        {
            try
            {
                _context.Vehicles.Remove(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete Vehicle {@Vehicle}", vehicle);
            }
        }
    }
}
