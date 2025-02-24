
using UnityEngine;
using TMPro;
using Game.ScriptableObjects;
public class TapsLeftView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private ReactiveInt _tapsSO;

    private void OnEnable()
    {
        _tapsSO.OnValueChanged += HandleValueChanged;
    }

    private void HandleValueChanged(int value)
    {
        _label.text = value.ToString();
    }
}
