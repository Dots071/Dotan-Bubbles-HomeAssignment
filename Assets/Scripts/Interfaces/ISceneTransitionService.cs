using Cysharp.Threading.Tasks;
using UnityEngine;

public interface ISceneTransitionService
{
    void Initialize(CanvasGroup transitionPanel);
    UniTask TransitionIn();
    UniTask TransitionOut();
} 