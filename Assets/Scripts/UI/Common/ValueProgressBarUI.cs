using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class ValueProgressBarUI : MonoBehaviour
    {
        [SerializeField] private Slider _valueSlider;
        [SerializeField] private TMP_Text _valueText;

        public void SetValue(float value, float maxValue)
        {
            _valueSlider.value = value / maxValue;
            _valueText.text = $"{value}/{maxValue}";
        }
    }
}