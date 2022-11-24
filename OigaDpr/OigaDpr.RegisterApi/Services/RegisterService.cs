using OigaDpr.RegisterApi.Infrastructure.Repositories;
using OigaDpr.RegisterApi.Models;

namespace OigaDpr.RegisterApi.Services
{
	public class RegisterService : IRegisterService
	{
		private ILogger<RegisterService> _logger;
		private IUserRepository _userRepository;
		public RegisterService(ILogger<RegisterService> logger, IUserRepository userRepository)
		{
			_logger = logger;
			_userRepository = userRepository;
		}

		public async Task<int> AddUser(User user)
		{
			try
			{
				await _userRepository.AddAsync(user);
				return 1;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error adding user");
				return -1;
			}
		}
	}
}
