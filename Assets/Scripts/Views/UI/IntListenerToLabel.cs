
using UnityEngine;
using TMPro;
using Game.ScriptableObjects;
public class IntListenerToLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private ReactiveInt _reactiveInt;

    private void OnEnable()
    {
        _label.text = _reactiveInt.Value.ToString();
        _reactiveInt.OnValueChanged += HandleValueChanged;
    }
    private void OnDisable()
    {
        _reactiveInt.OnValueChanged -= HandleValueChanged;
    }

    private void HandleValueChanged(int value)
    {
        _label.text = value.ToString();
    }
}
