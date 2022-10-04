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

        private const float Y_ROTATION_OFFSET = 0.5150381f;
        protected WeaponConfig _weaponConfig;
        private const float MILISECONDS = 0.1f;
        private Pool _bulletsPool;
        private Fighter _fighter;
        private int _currentBulletsCount;
        private bool _isReloading;
        private Coroutine _reloading;
        private GameObject _weaponModel;
        private Transform _shootingPoint;

        private void Awake()
        {
            _bulletsPool = GetComponentInChildren<Pool>(true);
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
            // var rotation = transform.parent.rotation;
            // var yRotation = rotation.y - Y_ROTATION_OFFSET;
            // rotation = new Quaternion(rotation.x, yRotation, rotation.z, rotation.w);
            //bulletTransform.rotation = rotation;
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
            OnStartReload?.Invoke();
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
            }

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