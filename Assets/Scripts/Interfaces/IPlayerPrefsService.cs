public interface IPlayerPrefsService
{
    void Save<T>(string key, T value);
    T Load<T>(string key) where T : new();
} 