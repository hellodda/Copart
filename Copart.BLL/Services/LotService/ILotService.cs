using Copart.BLL.Models.LotModels;
using Copart.BLL.Results;

namespace Copart.BLL.Services.LotService
{
    public interface ILotService
    {
        public Task<Result<IEnumerable<LotModel>>> GetAll(CancellationToken token = default);
        public Task<Result<LotModel>> GetById(int id, CancellationToken token = default);
        public Task<Result<LotModel>> GetByLotNumber(string lotNumber, CancellationToken token = default);
        public Task<Result> Update(int id, LotUpdateModel lot, CancellationToken token = default);
        public Task<Result> Add(LotAddModel lot, CancellationToken token = default);
        public Task<Result> Delete(int id, CancellationToken token = default);
    }
}
