using Copart.BLL.Models.BidModels;
using Copart.BLL.Results;
using FluentValidation;

namespace Copart.BLL.Services.BidService
{
    public interface IBidService
    {
        public Task<Result<IEnumerable<BidModel?>?>> GetAllAsync(CancellationToken token = default);
        public Task<Result<BidModel?>> GetByIdAsync(int id, CancellationToken token = default);
        public Task<Result> AddAsync(BidAddModel model, IValidator<BidAddModel> validator, CancellationToken token = default);
        public Task<Result> UpdateAsync(int id, BidUpdateModel model, CancellationToken token = default);
        public Task<Result> DeleteAsync(int id, CancellationToken token = default);
    }
}
