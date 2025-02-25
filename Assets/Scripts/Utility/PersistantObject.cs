
using UnityEngine;

/// <summary>
/// A helper class in case we would like to have persistant objects.
/// </summary>
namespace Game.Utility
{
    public class PersistantObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}

