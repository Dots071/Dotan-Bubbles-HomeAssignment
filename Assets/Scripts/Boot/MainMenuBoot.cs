using Game.Controllers;
using Game.Models;
using Game.Views;
using Game.ScriptableObjects;
using Game.Services;

using UnityEngine;

namespace Game.Boot
{
    // Sets up the main menu scene and connects UI elements to their controllers
    public class MainMenuBoot : MonoBehaviour
    {
        [SerializeField] private MainMenuView _view;
        [SerializeField] private AssetReferencesSO _assetReferences;
        [SerializeField] private GameConfigSO _gameConfig;

        private MainMenuController _controller;

        private ServiceLocatorSO _serviceLocator;

        async void Start()
        {
            _serviceLocator = await AddressablesService.LoadAssetAsync<ServiceLocatorSO>(_assetReferences.ServiceLocatorSO);
            _controller = new MainMenuController(_view, _gameConfig, _assetReferences, _serviceLocator);


        }

        private void OnDestroy()
        {
            _controller.Dispose();
        }
    }
}

