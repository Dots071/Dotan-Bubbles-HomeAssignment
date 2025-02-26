using Cysharp.Threading.Tasks;
using UnityEngine;

// Interface for scene transition effects including initialization and fade in/out
public interface ISceneTransitionService
{
    void Initialize(CanvasGroup transitionPanel);
    UniTask TransitionIn();
    UniTask TransitionOut();
} 