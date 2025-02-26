using Game.ScriptableObjects;
using UnityEngine;
using Game.Interfaces;
using Game.Services;
using Game.Models;
using System;

namespace Game.Controllers 
{
    // Handles main menu interactions including settings, play button, and difficulty selection
    public class MainMenuController : IDisposable
    {
        [SerializeField] private IMainMenuView _view;
        [SerializeField] private AssetReferencesSO _assetReferences;

        private GameConfigSO _gameConfig;
        private IPlayerPrefsService _prefsService;
        private ISceneManager _sceneManager;
        private ISceneTransitionService _transitionService;
        private ServiceLocatorSO _serviceLocator;

        private const string SETTINGS_KEY = "GameSettingsLocal";

        public MainMenuController(IMainMenuView view, GameConfigSO model, AssetReferencesSO assetReferences, ServiceLocatorSO serviceLocator)
        {
            _view = view;
            _gameConfig = model;
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
            var settings = _prefsService.Load<GameSettingsModel>(SETTINGS_KEY);
            _gameConfig.IsMusicOn = settings.IsMusicOn;
            _gameConfig.IsSfxOn = settings.IsSfxOn;
            _gameConfig.Difficulty = settings.Difficulty;
            _gameConfig.HighScore = settings.HighScore;
        }

        private void SetupView()
        {
            _view.Initialize();
            _view.SetHighScore(_gameConfig.HighScore);
            _view.UpdateSettings(_gameConfig.IsMusicOn, _gameConfig.IsSfxOn, _gameConfig.Difficulty);
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
            await _sceneManager.LoadSceneAsync(_assetReferences.GameplayScene, false);
        }

        public void OnSettingsClicked() => _view.ShowSettingsPanel(true);
        
        public void OnCloseSettingsClicked()
        {
            SaveSettings();
            _view.ShowSettingsPanel(false);
        }

        public void OnMusicToggled(bool isOn) => _gameConfig.IsMusicOn = isOn;

        public void OnSfxToggled(bool isOn) => _gameConfig.IsSfxOn = isOn;

        public void OnDifficultyChanged(int value) => _gameConfig.Difficulty = value;

        private void SaveSettings()
        {
            var settings = new GameSettingsModel();
            settings.IsMusicOn = _gameConfig.IsMusicOn;
            settings.IsSfxOn = _gameConfig.IsSfxOn;
            settings.Difficulty = _gameConfig.Difficulty;
            settings.HighScore = _gameConfig.HighScore;
            _prefsService.Save(SETTINGS_KEY, settings);

        }

        public void Dispose()
        {
            SaveSettings();
            UnSubscribeToEvents();
        }
    }
}

