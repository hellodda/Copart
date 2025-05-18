using Copart.UI.Models.UserModels;
using System.Net.Http.Json;

namespace Copart.UI.Apis.UserApi
{
    public class UserApi : IUserApi
    {
		private readonly HttpClient _client;

		public UserApi(HttpClient client)
		{
			_client = client;
		}

        public async Task Add(UserAddModel model, CancellationToken token = default)
        {
			try
			{
				await _client.PostAsJsonAsync<UserAddModel>("/user", model, token);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
        }

        public async Task<UserModel> GetByName(string name, CancellationToken token = default)
        {
			try
			{
				return await _client.GetFromJsonAsync<UserModel>($"/user/{name}", token);
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
                throw;
			}
        }
    }
}
