using System;
using System.Collections;
using Configs.Weapons;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public event Action<GameObject> OnEnemyFound;
        public event Action OnStartReload;
        public event Action<int> OnReload;
        public int Distance => _weaponConfig.ShootingDistance;
        public float ShootingSpeed => _weaponConfig.ShootingSpeed;
        public bool IsReloading => _isReloading;
        
        protected WeaponConfig _weaponConfig;
        private const float MILISECONDS = 0.1f;
        private Pool _bulletsPool;
        private Fighter _fighter;
        private int _currentBulletsCount;
        private bool _isReloading;
        private bool _isStartReloading;
        private Coroutine _reloading;
        private GameObject _weaponModel;
        private Transform _shootingPoint;
        private SphereCollider _collider;

        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
            _bulletsPool = GetComponentInChildren<Pool>(true);
            _collider.radius = _weaponConfig.ShootingDistance;
            _currentBulletsCount = _weaponConfig.BulletsCount;
        }

        public void Initialize(Transform modelPosition, Transform shootingPoint)
        {
            _weaponModel = Instantiate(_weaponConfig.WeaponModel, modelPosition);
            _weaponModel.transform.localPosition = Vector3.zero;
            _shootingPoint = shootingPoint;
        }

        public void Shoot(Vector3 direction)
        {
            if (_isReloading) return;
            
            var bullet = _bulletsPool.GetObject(null).GetComponent<Bullet>();
            var bulletTransform = bullet.transform;
            bulletTransform.position = _shootingPoint.position;
            bulletTransform.rotation = Quaternion.LookRotation(direction);
            bullet.Initialize(_weaponConfig.DamageValue, _weaponConfig.BulletSpeed, direction);

            if (--_currentBulletsCount <= 0)
            {
                Reload();
            }
        }

        private void Reload()
        {
            _isReloading = true;
            _reloading = StartCoroutine(Reloading());
        }

        private IEnumerator Reloading()
        {
            var reloadTime = 0f;

            while (reloadTime < _weaponConfig.ReloadTime)
            {
                reloadTime += MILISECONDS;
                OnReload?.Invoke((int)(reloadTime * 10f));
                yield return new WaitForSeconds(MILISECONDS);

                if (_isStartReloading) continue;
                
                OnStartReload?.Invoke();
                _isStartReloading = true;
            }

            _isStartReloading = false;
            _isReloading = false;
            _currentBulletsCount = _weaponConfig.BulletsCount;
            StopCoroutine(_reloading);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            OnEnemyFound?.Invoke(other.gameObject);
        }
    }
}