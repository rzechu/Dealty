namespace Dealty.WebApi.Interfaces
{
    public interface IPagination<T>
    {
        Task<IEnumerable<T>> GetAllPaginatedAsync(int fetch, int offset);

    }
}