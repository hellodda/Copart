using Copart.BLL.Models.LotModels;

namespace Copart.UI.Apis.LotApi
{
    public interface ILotApi
    {
        public Task<IEnumerable<LotModel>> GetAll(CancellationToken token = default);
    }
}
