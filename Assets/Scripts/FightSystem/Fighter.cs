using System;
using System.Collections;
using UnityEngine;
using Weapons;

[RequireComponent(typeof(Health))]
public class Fighter : MonoBehaviour
{
    public event Action OnStartFight;
    public event Action OnStopFight;
    public event Action<Transform> OnSetFightTarget;
    public event Action OnResetFightTarget;
    public event Action OnEnemyFound;
    
    public bool IsFight { get; private set; } = false;
    public bool IsAlive { get; private set; } = true;
    public Fighter CurrentEnemy => _currentEnemy;

    private const float DISTANCE_VARIATION = 2f;
    private Weapon _currentWeapon;
    private Fighter _currentEnemy;
    private Coroutine _fightCoroutine;
    private Vector3 _shootDirection;
    private bool _isCanShoot;
    private bool _isEnemyInitialized;

    public void Initialize(Fighter enemy = null)
    {
        if (enemy)
        {
            _currentEnemy = enemy;
            _isEnemyInitialized = true;
        }
        
        var weaponPosition = GetComponentInChildren<WeaponPlaceComponent>(true);
        var shootingPoint = GetComponentInChildren<ShootingPointComponent>(true);
        _currentWeapon = GetComponentInChildren<Weapon>(true);
        _currentWeapon.Initialize(this, weaponPosition.gameObject.transform,
            shootingPoint.gameObject.transform);
        ShootingActivate();
    }

    public void ShootingActivate()
    {
        _isCanShoot = true;
    }
    
    public void ShootingDeactivate()
    {
        _isCanShoot = false;
    }

    public void StopAttack()
    {
        OnStopFight?.Invoke();
    }

    private bool IsEnemyInAttackZone()
    {
        return (Vector3.Distance(gameObject.transform.position, 
            _currentEnemy.transform.position) < _currentWeapon.Distance + DISTANCE_VARIATION);
    }
    
    public void OnDie()
    {
        ShootingDeactivate();
        OnResetFightTarget?.Invoke();
        if (_fightCoroutine != null) StopCoroutine(_fightCoroutine);
        _currentWeapon.gameObject.SetActive(false);
        IsAlive = false;
        IsFight = false;
        _currentEnemy = null;
    }

    public void OnEnemyFounded(GameObject enemyObject)
    {
        if (IsFight) return;

        if (!_currentEnemy)
        {
            enemyObject.TryGetComponent(out Fighter enemy);
            _currentEnemy = enemy;
        }
        
        if (!_currentEnemy) return;
        
        OnEnemyFound?.Invoke();
        IsFight = true;
        OnSetFightTarget?.Invoke(_currentEnemy.transform);
        _fightCoroutine ??= StartCoroutine(Fighting());
    }
    
    public void OnEnemyFoundedOnShoot()
    {
        if (IsFight) return;

        if (!_currentEnemy) return;
        
        OnEnemyFound?.Invoke();
        IsFight = true;
        OnSetFightTarget?.Invoke(_currentEnemy.transform);
        _fightCoroutine ??= StartCoroutine(Fighting());
    }
    
    private IEnumerator Fighting()
    {
        while (_currentEnemy && _currentEnemy.IsAlive && IsFight)
        {
            _shootDirection =  _currentEnemy.transform.position - gameObject.transform.position;
            
            if (IsEnemyInAttackZone())
            {
                if (_currentEnemy)
                {
                    if (_currentEnemy.IsAlive && !_currentWeapon.IsReloading && _isCanShoot)
                    {
                        OnStartFight?.Invoke();
                        _currentWeapon.Shoot(_shootDirection.normalized);
                    }
                }
                yield return new WaitForSeconds(_currentWeapon.ShootingSpeed);
            }
            else
            {
                IsFight = false;
                OnResetFightTarget?.Invoke();
            }
        }
        
        OnResetFightTarget?.Invoke();
        IsFight = false;
        if (!_isEnemyInitialized) _currentEnemy = null;
        if (_fightCoroutine != null) StopCoroutine(_fightCoroutine);
        _fightCoroutine = null;
    }
}
