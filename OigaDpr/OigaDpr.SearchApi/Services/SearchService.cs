using OigaDpr.SearchApi.Infrastructure;
using OigaDpr.SearchApi.Models;

namespace OigaDpr.SearchApi.Services
{
    public class SearchService : ISearchService
    {
        private readonly ILogger<SearchService> _logger;
        private readonly IUserRepository _userRepository;

        public SearchService(IUserRepository userRepository, ILogger<SearchService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<User?> Get(string username)
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

        public async Task<IEnumerable<User>> Search(string filters, int pageSize, int pageIndex)
        {
            try
            {
                var users = (await _userRepository.GetAll()).ToList();
                return await PageResults(users, string.IsNullOrEmpty(filters) ? Array.Empty<string>() : filters.Split(" "), pageSize, pageIndex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user");
                return new List<User>();
            }
        }

        private async Task<List<User>> PageResults(List<User> users, string[] filters, int pageSize, int pageIndex)
        {
            var filteredList = new List<User>();
            if (filters.Any() && users.Any())
            {
                var tasks = filters.Select(f => Filter(users, f)).ToList();

                await Task.WhenAll(tasks);

                tasks.ForEach(t =>
                {
                    if (t.Result.Any())
                    {
                        filteredList.AddRange(t.Result);
                    }
                });
            }

            filteredList = filteredList.Skip(pageIndex).Take(pageSize).ToList();

            return filteredList;
        }

        private Task<List<User>> Filter(List<User> users, string data)
        {
            users = users.Where(u => u!.Username.Contains(data, StringComparison.InvariantCultureIgnoreCase)
                                     || u!.FullName.Contains(data, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            return Task.FromResult(users);
        }
    }
}
