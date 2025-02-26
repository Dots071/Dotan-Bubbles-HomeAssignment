using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    // UI for the main menu including buttons, panels, and animation effects
    public class MainMenuView : MonoBehaviour, IMainMenuView
    {
        [Header("Main Menu Elements")]
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private TextMeshProUGUI _highScoreText;

        [Header("Settings Panel")]
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private Button _closeSettingsButton;
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _sfxToggle;
        [SerializeField] private TMP_Dropdown _difficultyDropdown;

        [Header("Transitions")]
        [SerializeField] private CanvasGroup _transitionPanel;

        [Header("Animations")]
        [SerializeField] private RectTransform _menuContainer;
        [SerializeField] private RectTransform _settingsContainer;

        public event Action OnPlayClicked;
        public event Action OnSettingsClicked;
        public event Action OnCloseSettingsClicked;
        public event Action<bool> OnMusicToggled;
        public event Action<bool> OnSfxToggled;
        public event Action<int> OnDifficultyChanged;

        public void Initialize()
        {
            // Connect UI events to controller
            _playButton.onClick.AddListener(HandlePlayClicked);
            _settingsButton.onClick.AddListener(HandleSettingsClicked);
            _closeSettingsButton.onClick.AddListener(HandleCloseSettingsClicked);

            _musicToggle.onValueChanged.AddListener(HandleMusicToggled);
            _sfxToggle.onValueChanged.AddListener(HandleSfxToggled);
            _difficultyDropdown.onValueChanged.AddListener(HandleDifficultyChanged);

            // Initial setup
            _settingsPanel.SetActive(false);
        }

        private void HandlePlayClicked() => OnPlayClicked?.Invoke();
        private void HandleSettingsClicked() => OnSettingsClicked?.Invoke();
        private void HandleCloseSettingsClicked() => OnCloseSettingsClicked?.Invoke();
        private void HandleMusicToggled(bool isOn) => OnMusicToggled?.Invoke(isOn);
        private void HandleSfxToggled(bool isOn) => OnSfxToggled?.Invoke(isOn);
        private void HandleDifficultyChanged(int difficulty) => OnDifficultyChanged?.Invoke(difficulty);


        public void SetHighScore(int score)
        {
            _highScoreText.text = score.ToString();
        }

        public void UpdateSettings(bool isMusicOn, bool isSfxOn, int difficulty)
        {
            _musicToggle.isOn = isMusicOn;
            _sfxToggle.isOn = isSfxOn;
            _difficultyDropdown.value = difficulty;
        }

        public void ShowSettingsPanel(bool show)
        {
            _mainMenuPanel.SetActive(!show);
            _settingsPanel.SetActive(show);

            if (show)
            {
                // Animate settings panel entry
                _settingsContainer.localScale = Vector3.one * 0.8f;
                _settingsContainer.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
            }
        }


        public CanvasGroup GetTransitionPanel()
        {
            return _transitionPanel;
        }
    }
}
