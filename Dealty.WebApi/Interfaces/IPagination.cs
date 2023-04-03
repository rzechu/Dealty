using Dealty.Shared.Data;
using Dealty.Shared.Filters;

namespace Dealty.WebApi.Interfaces
{
    public interface IPagination<T>
    {
        Task<(IEnumerable<T>, int)> GetAllPaginatedAsync(PaginationFilter paginationFilter);
    }
}