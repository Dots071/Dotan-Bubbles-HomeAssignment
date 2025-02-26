using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.ScriptableObjects
{
    // Stores references to scene assets that can be loaded via the Addressables system
    [CreateAssetMenu(fileName = "ScenesSO", menuName = "Scriptable Objects/ScenesSO")]
    public class ScensSO : ScriptableObject
    {
        public AssetReference SceneReference;
    }
}

