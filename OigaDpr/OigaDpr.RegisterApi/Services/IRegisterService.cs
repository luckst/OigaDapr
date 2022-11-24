using OigaDpr.RegisterApi.Models;

namespace OigaDpr.RegisterApi.Services
{
    public interface IRegisterService
    {
        Task<int> AddUser(User user);
    }
}