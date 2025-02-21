using System;
using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewGameEvent", menuName = "Scriptable Objects/Events/Game Event")]
    public class GameEvent : ScriptableObject
    {
        public event Action OnEventRaised;


        public void Raise()
        {
            OnEventRaised?.Invoke();
        }
    }
}


