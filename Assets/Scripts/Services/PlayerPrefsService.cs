using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services 
{
    public class PlayerPrefsService : IPlayerPrefsService
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
            try
            {
                string data = JsonUtility.ToJson(value);
                PlayerPrefs.SetString(key, data);
                PlayerPrefs.Save();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save {key}: {ex.Message}");
            }
        }

        public T Load<T>(string key) where T : new()
        {
            try
            {
                if (PlayerPrefs.HasKey(key))
                {
                    string data = PlayerPrefs.GetString(key);
                    T result = JsonUtility.FromJson<T>(data);
                    return result != null ? result : new T();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load {key}: {ex.Message}");
            }
            
            return new T();
        }
    }
}
