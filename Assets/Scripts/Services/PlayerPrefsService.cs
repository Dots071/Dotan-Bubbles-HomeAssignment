using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services 
{
    public class PlayerPrefsService
    {
        private readonly Dictionary<Type, Action<string, object>> SaveActions = new()
        {
            { typeof(int), (key, value) => PlayerPrefs.SetInt(key, (int)value) },
            { typeof(float), (key, value) => PlayerPrefs.SetFloat(key, (float)value) },
            { typeof(string), (key, value) => PlayerPrefs.SetString(key, (string)value) },
            { typeof(bool), (key, value) => PlayerPrefs.SetInt(key, (bool)value ? 1 : 0) }
        };

        private readonly Dictionary<Type, Func<string, object>> LoadActions = new()
        {
            { typeof(int), key => PlayerPrefs.GetInt(key) },
            { typeof(float), key => PlayerPrefs.GetFloat(key) },
            { typeof(string), key => PlayerPrefs.GetString(key) },
            { typeof(bool), key => PlayerPrefs.GetInt(key) == 1 }
        };

        public void Save<T>(string key, T value)
        {
            if (SaveActions.TryGetValue(typeof(T), out var saveAction))
            {
                saveAction(key, value);
            }
            else
            {
                // Serialize complex objects to JSON
                PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
            }
        }

        public T Load<T>(string key)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                Debug.LogWarning($"[PlayerPrefsService] Key '{key}' does not exist. Returning default value.");
                return default;
            }

            try
            {
                if (LoadActions.TryGetValue(typeof(T), out var loadAction))
                {
                    return (T)loadAction(key);
                }

                // Deserialize complex types from JSON
                string jsonData = PlayerPrefs.GetString(key);
                return JsonUtility.FromJson<T>(jsonData);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[PlayerPrefsService] Error loading key '{key}': {ex.Message}");
                return default;
            }
        }
    }
}
