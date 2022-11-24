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
                var key = new Key();
                key.KeyValues.Add(user.Username);
                keyState.Value = key;
            }

            keyState.Value.KeyValues.Add(user.Username);
            await keyState.SaveAsync();
        }

        public async Task<User> Get(string username)
        {
            var state = await _daprClient.GetStateEntryAsync<User>(OigaStateStore, username);

            return state.Value;
        }

        public async Task<IEnumerable<User>> Search(string[] filters)
        {
            var key = await _daprClient.GetStateAsync<Key>(KeysStateStore, KeyName);

            if (key == null || !key.KeyValues.Any())
            {
                return new List<User>();
            }



            var state = await _daprClient.GetBulkStateAsync(OigaStateStore, key.KeyValues, 2);

            return new List<User>();
        }
    }
}
