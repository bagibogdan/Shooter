using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIHealthView : MonoBehaviour
    {
        private Slider _healthSlider;
        private TextMeshProUGUI _healthText;

        private void Awake()
        {
            _healthSlider = gameObject.GetComponentInChildren<Slider>();
            _healthText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Initialize(Health health)
        {
            _healthSlider.maxValue = health.MaxHealthValue;
            _healthSlider.value = _healthSlider.maxValue;
            _healthText.text = _healthSlider.maxValue.ToString();
        }

        public void HealthView(int healthValue)
        {
            _healthSlider.value = healthValue;
            _healthText.text = healthValue.ToString();
        
            if (_healthSlider.value == 0)
            {
                _healthSlider.gameObject.SetActive(false);
            }
        }
    }
}
