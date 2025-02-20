using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log($"[ServiceLocatorSO] Service of type {type} is registered.");
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

    /// <summary>
    /// Iterates over all registered services and initializes them asynchronously.
    /// Only services inheriting from ServiceBase will be initialized.
    /// </summary>
    public async UniTask InitializeAllServicesAsync()
    {
        foreach (var service in _services.Values)
        {
            if (service is ServiceBase s)
            {
                await s.InitializeAsync();
            }
        }
    }

    /// <summary>
    /// Clears all registered services.
    /// </summary>
    public void ClearServices()
    {
        _services.Clear();
    }
}
