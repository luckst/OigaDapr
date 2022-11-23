using OigaDpr.Common.Dtos.User;

namespace OigaDpr.Data.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
    }
}
