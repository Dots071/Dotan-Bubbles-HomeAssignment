
using UnityEngine;

namespace Game.Interfaces
{
    /// <summary>
    /// Generic factory interface for creating instances of T.
    /// </summary>
    public interface IFactory<T> where T : Component
    {
        T Create();
    }
}

