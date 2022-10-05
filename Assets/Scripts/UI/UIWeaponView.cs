using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class UIWeaponView : MonoBehaviour
    {
        private Weapon _weapon;
        private TextMeshProUGUI _weaponText;
        private Slider _reloadSlider;
        private int _timeInteger;
        private int _timeFractional;
        private int _previousTime;

        private void Awake()
        {
            _reloadSlider = gameObject.GetComponentInChildren<Slider>();
            _weaponText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        }
        
        public void Initialize(Weapon weapon)
        {
            _weapon = weapon;
            _reloadSlider.maxValue = _weapon.BulletsCount;
            _reloadSlider.value = _reloadSlider.maxValue;
            _weaponText.text = _reloadSlider.maxValue.ToString();
        }
        
        public void BulletsView(int bulletsValue)
        {
            if (!Mathf.Approximately(_reloadSlider.maxValue, _weapon.BulletsCount))
            {
                _reloadSlider.maxValue = _weapon.BulletsCount;
            }
            
            _reloadSlider.value = bulletsValue;
            _weaponText.text = bulletsValue.ToString();
        
            if (_reloadSlider.value != 0) return;
            
            _reloadSlider.maxValue = _weapon.ReloadTime * 10f;
            _reloadSlider.value = _reloadSlider.minValue;
        }

        public void ReloadView(int time)
        {
            _timeInteger = time / 10;
            _timeFractional = time % 10;
            _weaponText.text = string.Concat(_timeInteger, ".", _timeFractional);
            _reloadSlider.value += _previousTime - time;
            _previousTime = time;
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}