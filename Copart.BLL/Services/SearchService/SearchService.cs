using Copart.BLL.Results;
using System.Reflection;

namespace Copart.BLL.Services.SearchService
{
    public class SearchService : ISearchService
    {
        private readonly IServiceProvider _serviceProvider;

        public SearchService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Result<IEnumerable<T>>> Search<T, ST>(string query) where ST : IService
        {
            try
            {
                var service = (ST)_serviceProvider.GetService(typeof(ST))!;
                if (service == null)
                    return Result<IEnumerable<T>>.Fail("Canot find service");

                var method = typeof(ST).GetMethod("GetAllAsync");
                if (method == null)
                    return Result<IEnumerable<T>>.Fail("Method Not Found");

                var task = (Task<Result<IEnumerable<T>>>)method.Invoke(service, [CancellationToken.None])!;
                var allItems = (await task).Data;

                var filteredItems = allItems!.Where(item =>
                    item!.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(p => p.PropertyType == typeof(string))
                        .Select(p => (string)p.GetValue(item)!)
                        .Any(value => value != null && value.Contains(query, StringComparison.OrdinalIgnoreCase))
                );

                return Result<IEnumerable<T>>.Ok(filteredItems);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<T>>.Fail($"Search Error: {ex.Message}");
            }
        }
    }
}
