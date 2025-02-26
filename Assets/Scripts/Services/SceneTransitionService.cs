using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;


namespace Game.Services
{
    public class SceneTransitionService : ISceneTransitionService
    {
        private CanvasGroup _transitionPanel;
        private const float _transitionDuration = 0.5f;

        public void Initialize(CanvasGroup transitionPanel)
        {
            _transitionPanel = transitionPanel;
            _transitionPanel.alpha = 0f;
            _transitionPanel.blocksRaycasts = false;
        }

        public async UniTask TransitionOut()
        {
            _transitionPanel.blocksRaycasts = true;
            await _transitionPanel.DOFade(1f, _transitionDuration).SetEase(Ease.InBack).AsyncWaitForCompletion();
        }

        public async UniTask TransitionIn()
        {
            await _transitionPanel.DOFade(0f, _transitionDuration).SetEase(Ease.InQuad).AsyncWaitForCompletion();
             _transitionPanel.blocksRaycasts = false;
        }
    }
}