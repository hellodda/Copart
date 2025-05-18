using Copart.UI.Apis.LotApi;
using Copart.UI.Apis.SearchApi;
using Copart.UI.Apis.UserApi;
using Copart.UI.Apis.VehicleApi;

namespace Copart.UI.Extensions
{
    public static class ProjectHttpClientsExtnesion
    {
        public static void AddProjectClients(this IServiceCollection services, Uri baseAddress)
        {
            services.AddHttpClient<ILotApi, LotApi>(client =>
            {
                 client.BaseAddress = baseAddress;
            });
            services.AddHttpClient<ISearchApi, SearchApi>(client =>
            {
                client.BaseAddress = baseAddress;
            });
            services.AddHttpClient<IVehicleApi, VehicleApi>(client =>
            {
                client.BaseAddress = baseAddress;
            });
            services.AddHttpClient<IUserApi, UserApi>(client =>
            {
                client.BaseAddress = baseAddress;
            });
        }
    }
}
