using Copart.UI.Models.UserModels;

namespace Copart.UI.Apis.UserApi
{
    public interface IUserApi
    {
        public Task Add(UserAddModel model, CancellationToken token = default);
        public Task<UserModel> GetByName(string name, CancellationToken token = default);
    }
}
