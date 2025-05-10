using Copart.Domain.BaseRepositories;

namespace Copart.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        CopartDbContext _context;

        public UnitOfWork(
            CopartDbContext context,
            IVehicleRepository vehicleRepository,
            ILotRepository lotRepository
        )
        {
            _context = context;

            VehicleRepository = vehicleRepository;
            LotRepository = lotRepository;
        }

        public IVehicleRepository VehicleRepository { get; set; }
        public ILotRepository LotRepository { get; set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Save(CancellationToken token = default)
        {
            await _context.SaveChangesAsync(token);
        }
    }
}
