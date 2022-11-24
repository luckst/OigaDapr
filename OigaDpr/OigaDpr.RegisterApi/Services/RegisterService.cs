using System.Data.SqlTypes;
using Dapr.Client;
using OigaDpr.RegisterApi.Infrastructure.NamedExceptions;
using OigaDpr.RegisterApi.Infrastructure.Repositories;
using OigaDpr.RegisterApi.Models;

namespace OigaDpr.RegisterApi.Services
{
	public class RegisterService : IRegisterService
	{
		private readonly ILogger<RegisterService> _logger;
		private readonly IUserRepository _userRepository;
        private readonly DaprClient _daprClient;

		public RegisterService(ILogger<RegisterService> logger, IUserRepository userRepository, DaprClient daprClient)
		{
			_logger = logger;
			_userRepository = userRepository;
            _daprClient = daprClient;
        }

		public async Task<int> AddUser(User user)
		{
			try
            {
                var searchResult =
                    await _daprClient.InvokeMethodAsync<User>(HttpMethod.Get, "searchapi",
                        $"api/search/{user.Username}");

                if (searchResult != null && !string.IsNullOrEmpty(searchResult.Username))
                    throw new UsernameTakenException();

				await _userRepository.AddAsync(user);
				return 1;
			}
            catch (UsernameTakenException ex)
            {
                _logger.LogInformation($"Username {user.Username} is already taken");
                throw;
            }
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error adding user");
				return -1;
			}
		}
	}
}
