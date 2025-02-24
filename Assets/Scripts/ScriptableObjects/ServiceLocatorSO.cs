using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ServiceLocatorSO", menuName = "Scriptable Objects/ServiceLocator")]
    public class ServiceLocatorSO : ScriptableObject
    {
        private Dictionary<Type, object> _services = new Dictionary<Type, object>();

        /// <summary>
        /// Registers a service instance with the locator.
        /// </summary>
        public void RegisterService<T>(T service)
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                Debug.LogWarning($"ServiceLocatorSO: Service of type {type} is already registered.");
                return;
            }
            foreach (var kvp in _services) 
            { 
            
            Debug.Log($"[ServiceLocatorSO] Service of type {kvp.Value} is registered.");
            }
            _services[type] = service;
        }

        /// <summary>
        /// Retrieves a registered service.
        /// </summary>
        public T GetService<T>()
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var service))
            {
                return (T)service;
            }
            Debug.LogError($"ServiceLocatorSO: Service of type {type} not found!");
            return default;
        }

        /// <summary>
        /// Unregisters a service from the locator.
        /// </summary>
        public void UnregisterService<T>()
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                _services.Remove(type);
            }
        }

        public void ClearServices()
        {
            Debug.Log($"[ServiceLocatorSO] Services cleared.");
            _services.Clear();
        }

        private void OnDestroy()
        {
            ClearServices();
        }
    }
}
