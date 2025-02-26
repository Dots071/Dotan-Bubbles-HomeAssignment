using Game.ScriptableObjects;
using UnityEngine;
using Game.Interfaces;
using Game.Services;
using Game.Models;
using System;

namespace Game.Controllers 
{
    public class MainMenuController : IDisposable
    {
        [SerializeField] private IMainMenuView _view;
        [SerializeField] private AssetReferencesSO _assetReferences;

        private GameSettingsModel _settings;
        private IPlayerPrefsService _prefsService;
        private ISceneManager _sceneManager;
        private ISceneTransitionService _transitionService;
        private ServiceLocatorSO _serviceLocator;

        private const string SETTINGS_KEY = "GameSettings";

        public MainMenuController(IMainMenuView view, GameSettingsModel model, AssetReferencesSO assetReferences, ServiceLocatorSO serviceLocator)
        {
            _view = view;
            _settings = model;
            _assetReferences = assetReferences;
            _serviceLocator = serviceLocator;

            Start();
        }

        private void Start()
        {
            InitializeServices();
            LoadSettings();
            SetupView();
            SubscribeToEvents();
        }

        private void InitializeServices()
        {
            // Get required services
            _sceneManager = _serviceLocator.GetService<ISceneManager>();
            _prefsService = _serviceLocator.GetService<IPlayerPrefsService>();
            
            // Initialize transition service
            _transitionService = new SceneTransitionService();
            var transitionPanel = _view.GetTransitionPanel();
            if (transitionPanel != null)
            {
                Debug.Log("There was a problem with finding the transition panel.");
            }
            _transitionService.Initialize(transitionPanel);
        }

        private void LoadSettings()
        {
            _settings = _prefsService.Load<GameSettingsModel>(SETTINGS_KEY);
        }

        private void SetupView()
        {
            _view.Initialize();
            _view.SetHighScore(_settings.HighScore);
            _view.UpdateSettings(_settings.IsMusicOn, _settings.IsSfxOn, _settings.Difficulty);
        }

        private void SubscribeToEvents()
        {
            _view.OnPlayClicked += OnPlayClicked;
            _view.OnSettingsClicked += OnSettingsClicked;
            _view.OnCloseSettingsClicked += OnCloseSettingsClicked;
            _view.OnMusicToggled += OnMusicToggled;
            _view.OnSfxToggled += OnSfxToggled;
            _view.OnDifficultyChanged += OnDifficultyChanged;
        }

        private void UnSubscribeToEvents()
        {
            _view.OnPlayClicked -= OnPlayClicked;
            _view.OnSettingsClicked -= OnSettingsClicked;
            _view.OnCloseSettingsClicked -= OnCloseSettingsClicked;
            _view.OnMusicToggled -= OnMusicToggled;
            _view.OnSfxToggled -= OnSfxToggled;
            _view.OnDifficultyChanged -= OnDifficultyChanged;
        }

        // UI Event Handlers
        public async void OnPlayClicked()
        {
            SaveSettings();
            await _transitionService.TransitionOut();
            await _sceneManager.LoadSceneAsync(_assetReferences.GameplayScene, true);
        }

        public void OnSettingsClicked() => _view.ShowSettingsPanel(true);
        
        public void OnCloseSettingsClicked()
        {
            SaveSettings();
            _view.ShowSettingsPanel(false);
        }

        public void OnMusicToggled(bool isOn) => _settings.IsMusicOn = isOn;

        public void OnSfxToggled(bool isOn) => _settings.IsSfxOn = isOn;

        public void OnDifficultyChanged(int value) => _settings.Difficulty = value;

        private void SaveSettings() => _prefsService.Save(SETTINGS_KEY, _settings);

        public void Dispose()
        {
            SaveSettings();
            UnSubscribeToEvents();
        }
    }
}

