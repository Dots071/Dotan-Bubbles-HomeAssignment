
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// One place to hold references to all Addressable assets (sprites, UI prefabs, scenes, etc.)
/// </summary>
[CreateAssetMenu(fileName = "AssetReferencesSO", menuName = "Scriptable Objects/AssetReferencesSO")]
public class AssetReferencesSO : ScriptableObject
{
    [Header("Scenes")]
    public AssetReference LoadingScene;
    public AssetReference MenuScene;
    public AssetReference GameplayScene;
    public AssetReference GameplayHudScene;

    [Header("Backgrounds")]
    public AssetReference background1;
    public AssetReference background2;

    [Header("Scriptable Objects")]
    public AssetReference BallsSO;
    public AssetReference BallsObjectPool;
    public AssetReference ServiceLocatorSO;

/*
    [Header("UI Elements")]
    public AssetReference closeButton;
    public AssetReference box;
    public AssetReference generalFrame1;
    public AssetReference generalFrame2;
    public AssetReference icon1;
    public AssetReference icon2;

    [Header("Audio")]
    public AssetReference backgroundMusic;
    public AssetReference sfxExplosion;
    public AssetReference musicIcon;*/
}
