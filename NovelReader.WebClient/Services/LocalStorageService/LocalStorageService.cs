using Blazored.LocalStorage;
using NovelReader.UI.Common.Services.LocalStorageService;

namespace NovelReader.WebClient.Services.LocalStorageService
{
    public class LocalStorageService : IStorageService
    {
        private readonly ILocalStorageService m_LocalStorage;
        public LocalStorageService(ILocalStorageService localStorageService)
        {
            m_LocalStorage = localStorageService; 
        }
        public async Task Clear()
        {
            await m_LocalStorage.ClearAsync();
        }

        public async Task<T> Get<T>(string key, T defaultValue)
        {
            var res = await m_LocalStorage.GetItemAsync<T>(key);

            if (res == null)
            {
                return defaultValue;
            }
            else
            {
                return res;
            }
        }

        public async Task Remove(string key)
        {
            await m_LocalStorage.RemoveItemAsync(key);
        }

        public async Task Set<T>(string key, T value)
        {
            await m_LocalStorage.SetItemAsync(key, value);
        }
    }
}
