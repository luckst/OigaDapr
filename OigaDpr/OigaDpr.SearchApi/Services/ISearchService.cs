using OigaDpr.SearchApi.Models;

namespace OigaDpr.SearchApi.Services
{
    public interface ISearchService
    {
        Task<User?> Get(string username);
        Task<PaginatedUserList> Search(string filters, int pageSize, int pageIndex);
    }
}