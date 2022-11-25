using OigaDpr.SearchApi.Infrastructure;
using OigaDpr.SearchApi.Models;
using static Google.Rpc.Context.AttributeContext.Types;

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

        public async Task<PaginatedUserList> Search(string filters, int pageSize, int pageIndex)
        {
            try
            {
                var users = (await _userRepository.GetAll()).ToList();
                return await PageResults(users, string.IsNullOrEmpty(filters) ? Array.Empty<string>() : filters.Split(" "), pageSize, pageIndex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user");
                return new PaginatedUserList();
            }
        }

        private async Task<PaginatedUserList> PageResults(List<User> users, string[] filters, int pageSize, int pageIndex)
        {
            var filteredList = new List<User>();
            if (filters.Any() && users.Any())
                filteredList = await FilterResults(users, filters);
            else
                filteredList.AddRange(users);

            var result = new PaginatedUserList()
            {
                TotalPages = (int)Math.Ceiling(filteredList.Count / (double)pageSize),
                PageIndex = pageIndex,
                Users = filteredList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()

            };

            return result;
        }

        private async Task<List<User>> FilterResults(List<User> users, string[] filters)
        {
            var filteredList = new List<User>();

            var tasks = filters.Select(f => Filter(users, f)).ToList();

            await Task.WhenAll(tasks);

            tasks.ForEach(t =>
            {
                if (t.Result.Any())
                {
                    filteredList.AddRange(t.Result);
                }
            });

            return filteredList.DistinctBy(u => u.Username).ToList();
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
