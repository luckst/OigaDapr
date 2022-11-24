using OigaDpr.SearchApi.Infrastructure;
using OigaDpr.SearchApi.Models;

namespace OigaDpr.SearchApi.Services
{
    public class SearchService : ISearchService
    {
        private ILogger<SearchService> _logger;
        private readonly IUserRepository _userRepository;

        public SearchService(IUserRepository userRepository, ILogger<SearchService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<User> Get(string username)
        {
            try
            {
                return await _userRepository.Get(username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user");
                return null;
            }
        }

        public async Task<IEnumerable<User>> Search(string filters)
        {
            try
            {
                return await _userRepository.Search(filters.Split(" "));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user");
                return new List<User>();
            }
        }
    }
}
