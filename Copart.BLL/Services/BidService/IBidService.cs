using Copart.BLL.Models.BidModels;
using Copart.BLL.Results;

namespace Copart.BLL.Services.BidService
{
    public interface IBidService
    {
        public Task<Result> Add(BidAddModel bid, CancellationToken token = default);
        public Task<Result> Update(int id, BidUpdateModel bid, CancellationToken token = default);
        public Task<Result> Delete(int id, CancellationToken token = default);
        public Task<Result<BidModel>> GetById(int id, CancellationToken token = default);
        public Task<Result<IEnumerable<BidModel>>> GetAll(CancellationToken token = default);
    }
}
