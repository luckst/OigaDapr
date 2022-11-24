using Dapr.Client;
using OigaDpr.RegisterApi.Models;

namespace OigaDpr.RegisterApi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DaprClient _daprClient;

        private const string OigaStateStore = "user-state";
        private const string KeysStateStore = "keys-state";
        private const string KeyName = "Usernames";

        public UserRepository(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task AddAsync(User user)
        {
            await _daprClient.SaveStateAsync(OigaStateStore, user.Username, user);

            var keyState = await _daprClient.GetStateEntryAsync<Key>(KeysStateStore, KeyName);

            if (keyState.Value == null)
            {
                keyState.Value = new Key();
            }

            if (!keyState.Value.KeyValues.Contains(user.Username, StringComparer.InvariantCultureIgnoreCase))
                keyState.Value.KeyValues.Add(user.Username);

            await keyState.SaveAsync();
        }

        public async Task<User> Get(string username)
        {
            var state = await _daprClient.GetStateEntryAsync<User>(OigaStateStore, username);

            return state.Value;
        }
    }
}
