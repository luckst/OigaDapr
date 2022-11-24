using OigaDpr.SearchApi.Models;

namespace OigaDpr.SearchApi.Services
{
    public interface ISearchService
    {
        Task<User?> Get(string username);
        Task<IEnumerable<User>> Search(string filters, int pageSize, int pageIndex);
    }
}