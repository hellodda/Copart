using Copart.UI.Models.LotModels;

namespace Copart.UI.Apis.LotApi
{
    public interface ILotApi
    {
        public Task<IEnumerable<LotModel>> GetAll(CancellationToken token = default);
        public Task<LotModel> GetLotByNumber(string number, CancellationToken token = default);
    }
}
