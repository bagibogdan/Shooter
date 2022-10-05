using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class UIWeaponView : MonoBehaviour
    {
        private Slider _reloadSlider;
        private TextMeshProUGUI _weaponText;
        private Weapon _weapon;
        private float _time;
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
            _reloadSlider.value = bulletsValue;
            _weaponText.text = bulletsValue.ToString();
        
            if (_reloadSlider.value == 0)
            {
                _reloadSlider.maxValue = _weapon.ReloadTime;
                _reloadSlider.value = _reloadSlider.minValue;
                _time = _weapon.ReloadTime;
            }
        }

        public void ReloadView(int time)
        {
            _time -= time;
            _time /= 10f;
            _weaponText.text = _time.ToString();
            _reloadSlider.value = time;
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}