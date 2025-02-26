// Interface for saving and loading persistent data with generic type support
public interface IPlayerPrefsService
{
    void Save<T>(string key, T value);
    T Load<T>(string key) where T : new();
} 