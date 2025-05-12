namespace Copart.Domain.BaseRepositories
{
    public interface IUnitOfWork
    {
        public IVehicleRepository VehicleRepository { get; set; }

        public ILotRepository LotRepository { get; set; }

        public IBidRepository BidRepository { get; set; }

        public IUserRepository UserRepository { get; set; }

        public Task Save(CancellationToken token = default);
        public void Dispose();
    }
}
