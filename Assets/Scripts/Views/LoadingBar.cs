
using UnityEngine;
using UnityEngine.UI;
using Game.ScriptableObjects;

namespace Game.Views
{
    public class LoadingBar : MonoBehaviour
    {
        [SerializeField] private Slider _loadingBar;
        [SerializeField] private ReactiveFlaot _progressFloat;
        private void OnEnable()
        {
            _progressFloat.OnValueChanged += UpdateProgress;
        }

        private void OnDisable()
        {
            _progressFloat.OnValueChanged -= UpdateProgress;
        }

        private void UpdateProgress(float progress)
        {
            _loadingBar.value = progress;
        }
    }
}
