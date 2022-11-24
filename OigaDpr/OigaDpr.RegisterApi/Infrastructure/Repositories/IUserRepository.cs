using OigaDpr.RegisterApi.Models;

namespace OigaDpr.RegisterApi.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
    }
}
