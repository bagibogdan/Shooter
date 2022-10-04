using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealthValue => _maxHealthValue;
    
    public event Action OnDie;
    public event Action<int> OnHealthChanged;
    public event Action<int> OnTakeDamage;
    
    private int _maxHealthValue;
    private int _currentHealthValue;

    public void Initialize(int maxHealthValue)
    {
        _maxHealthValue = maxHealthValue;
        _currentHealthValue = _maxHealthValue;
    }

    public void TakeDamage(int damageValue)
    {
        if (damageValue < 0) return;

        _currentHealthValue -= damageValue;
        OnTakeDamage?.Invoke(damageValue);

        if (_currentHealthValue < 0) _currentHealthValue = 0;
        
        OnHealthChanged?.Invoke(_currentHealthValue);

        if (_currentHealthValue == 0)
        {
            OnDie?.Invoke();
        }
    }
}
