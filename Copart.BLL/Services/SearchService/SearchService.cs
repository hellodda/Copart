using Copart.BLL.Attributes;
using Copart.BLL.Results;
using Microsoft.Extensions.DependencyInjection;
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
                var service = _serviceProvider.GetRequiredService<ST>();
                if (service is null)
                    return Result<IEnumerable<T>>.Fail("Cannot find service");

                var methods = service.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes(typeof(UseForSearchAttribute), inherit: true);
                    if (attributes is null || !attributes.Any())
                        continue;

                    if (method.ReturnType != typeof(Task<Result<IEnumerable<T>>>))
                        continue;

                    var task = (Task<Result<IEnumerable<T>>>)method.Invoke(service, new object[] { CancellationToken.None })!;
                    var result = await task;
                    var allItems = result.Data;

                    if (allItems is null)
                        return Result<IEnumerable<T>>.Fail("No data returned from search method");

                    var filteredItems = allItems.Where(item =>
                        item!.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(p => p.PropertyType == typeof(string))
                        .Select(p => (string)p.GetValue(item)!)
                        .Any(value => value != null && value.Contains(query, StringComparison.OrdinalIgnoreCase))
                    );

                    return Result<IEnumerable<T>>.Ok(filteredItems);
                }

                return Result<IEnumerable<T>>.Fail("No suitable method with [UseForSearch] found.");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<T>>.Fail($"Search Error: {ex.Message}");
            }
        }

    }
}
