using System;
using UnityEngine;

namespace Game.ScriptableObjects 
{
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "Scriptable Objects/Variables/FloatVariable")]
    public class ReactiveFlaot : ScriptableObject
    {
        [SerializeField] private float _value;

        // Event that is fired whenever the value changes.
        public event Action<float> OnValueChanged;

        public float Value
        {
            get => _value;
            set
            {
                // Use Mathf.Approximately for float comparisons.
                if (!Mathf.Approximately(_value, value))
                {
                    _value = value;
                    OnValueChanged?.Invoke(_value);
                }
            }
        }
    }
}


