using Copart.UI.Models.BidModels;
using Copart.UI.Models.LotModels;

namespace Copart.UI.Apis.LotApi
{
    public interface ILotApi
    {
        public Task<IEnumerable<LotModel>> GetAll(CancellationToken token = default);
        public Task<IEnumerable<BidModel>> GetAllBids(int id, CancellationToken token = default);
        public Task<LotModel> GetLotByNumber(string number, CancellationToken token = default);
        public Task<BidModel> GetBiggestBid(int id, CancellationToken token = default);
        public Task AddBid(int id, BidAddModel bid, CancellationToken token = default);
    }
}
