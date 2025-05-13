using Copart.BLL.Results;

namespace Copart.BLL.Services.SearchService
{
    public interface ISearchService : IService
    {
        public Task<Result<IEnumerable<T>>> Search<T, ST>(string query) where ST : IService;
    }
}
