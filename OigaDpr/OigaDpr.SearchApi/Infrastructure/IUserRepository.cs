using OigaDpr.SearchApi.Models;

namespace OigaDpr.SearchApi.Infrastructure
{
    public interface IUserRepository
    {
        Task<User?> Get(string username);
        Task<IEnumerable<User>> GetAll();
    }
}