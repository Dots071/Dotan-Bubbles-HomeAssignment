
using System;
using UnityEngine;

public interface IMainMenuView
{
    event Action OnPlayClicked;
    event Action OnSettingsClicked;
    event Action OnCloseSettingsClicked;
    event Action<bool> OnMusicToggled;
    event Action<bool> OnSfxToggled;
    event Action<int> OnDifficultyChanged;

    void Initialize();
    void SetHighScore(int score);
    void UpdateSettings(bool isMusicOn, bool isSfxOn, int difficulty);
    void ShowSettingsPanel(bool show);
    CanvasGroup GetTransitionPanel();

}
