using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ScenesSO", menuName = "Scriptable Objects/ScenesSO")]
    public class ScensSO : ScriptableObject
    {
        public AssetReference SceneReference;
    }
}

