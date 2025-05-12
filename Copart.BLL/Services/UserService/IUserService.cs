using Copart.BLL.Models.UserModels;
using Copart.BLL.Results;

namespace Copart.BLL.Services.BidderService
{
    public interface IUserService
    {
        public Task<Result> Add(UserAddModel user, CancellationToken token = default);
        public Task<Result> Delete(int id, CancellationToken token = default);
        public Task<Result<IEnumerable<UserModel>>> GetAll(CancellationToken token = default);
        public Task<Result<UserModel>> GetById(int id, CancellationToken token = default);
        public Task<Result> Update(int id, UserUpdateModel user, CancellationToken token = default);
    }
}
