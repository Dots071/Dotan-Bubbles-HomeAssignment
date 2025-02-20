using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameEvent", menuName = "Scriptable Objects/Events/Game Event")]
public class GameEvent : ScriptableObject
{
    private readonly List<Action> _listeners = new List<Action>();

    public void Raise()
    {
        foreach (var listener in new List<Action>(_listeners))
        {
            listener?.Invoke();
        }
    }

    public void RegisterListener(Action listener)
    {
        if (!_listeners.Contains(listener))
        {
            _listeners.Add(listener);
        }
    }

    public void UnregisterListener(Action listener)
    {
        if (_listeners.Contains(listener))
        {
            _listeners.Remove(listener);
        }
    }

    public void UnregisterAllListeners()
    {
        _listeners.Clear();
    }
}
