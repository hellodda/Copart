using Copart.Domain.BaseRepositories;
using Copart.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Copart.Data.Repositories
{
    public sealed class VehicleRepository : IVehicleRepository
    {
        private readonly CopartDbContext _context;

        public VehicleRepository(CopartDbContext context)
        {
            _context = context;
        }

        public IQueryable<Vehicle> Query()
        {
            return _context.Vehicles.AsNoTracking();
        }

        public async Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken = default)
        {
            await _context.Vehicles.AddAsync(vehicle, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Vehicle?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Vehicles
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Vehicle?>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Vehicles
                .AsNoTracking()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Vehicle?>?> GetByMakeAsync(string make, CancellationToken cancellationToken = default)
        {
            return await _context.Vehicles
                .AsNoTracking()
                .Where(v => v.Make == make)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Vehicle?>?> GetByModelAsync(string model, CancellationToken cancellationToken = default)
        {
            return await _context.Vehicles
                .AsNoTracking()
                .Where(v => v.Model == model)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Vehicle?> GetByVinAsync(string vin, CancellationToken cancellationToken = default)
        {
            return await _context.Vehicles
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Vin == vin, cancellationToken)
                .ConfigureAwait(false);
        }

        public Task UpdateAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
            return Task.CompletedTask;
        }
    }
}