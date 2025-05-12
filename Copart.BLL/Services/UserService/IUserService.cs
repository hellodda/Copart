using Copart.BLL.Models.BidModels;
using Copart.BLL.Models.UserModels;
using Copart.BLL.Results;
using FluentValidation;

namespace Copart.BLL.Services.BidderService
{
    public interface IUserService
    {
        public Task<Result<IEnumerable<UserModel?>?>> GetAllAsync(CancellationToken token = default);
        public Task<Result<UserModel?>> GetByIdAsync(int id, CancellationToken token = default);
        public Task<Result> AddAsync(UserAddModel model, IValidator<UserAddModel> validator, CancellationToken token = default);
        public Task<Result> AddBidAsync(int id, BidAddModel model, CancellationToken token = default);
        public Task<Result> UpdateAsync(int id, UserUpdateModel model, CancellationToken token = default);
        public Task<Result> DeleteAsync(int id, CancellationToken token = default);
    }
}
