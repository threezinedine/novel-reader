namespace NovelReader.UI.Common.Services.LocalStorageService
{
    public interface IStorageService
    {
        public Task<T> Get<T>(string key, T defaultValue);
        public Task Set<T>(string key, T value);
        public Task Remove(string key);
        public Task Clear();
    }
}
