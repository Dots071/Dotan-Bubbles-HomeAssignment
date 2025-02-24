using System;
using UnityEngine;

namespace Game.ScriptableObjects 
{
    [CreateAssetMenu(fileName = "ReactiveInt", menuName = "Scriptable Objects/Variables/Reactive Int")]
    public class ReactiveInt : ScriptableObject
    {
        [SerializeField] private int _value;

        public event Action<int> OnValueChanged;

        public int Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnValueChanged?.Invoke(_value);
                }
            }
        }
    }
}


