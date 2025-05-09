namespace Copart.Domain.BaseRepositories
{
    public interface IUnitOfWork
    {
        public IVehicleRepository VehicleRepository { get; set; }

        public Task Save(CancellationToken token = default);
        public void Dispose();
    }
}
