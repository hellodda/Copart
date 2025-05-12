using Copart.Domain.BaseRepositories;

namespace Copart.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        CopartDbContext _context;

        public UnitOfWork(CopartDbContext context)
        {
            _context = context;

            VehicleRepository = new VehicleRepository(context);
            LotRepository = new LotRepository(context);
            BidRepository = new BidRepository(context);
            UserRepository = new UserRepository(context);

        }

        public IVehicleRepository VehicleRepository { get; set; }
        public ILotRepository LotRepository { get; set; }
        public IBidRepository BidRepository { get; set; }
        public IUserRepository UserRepository { get; set; }

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
