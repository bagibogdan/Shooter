using UnityEngine;

namespace Configs.Weapons
{
    public abstract class WeaponConfig : ScriptableObject
    {
        [SerializeField] protected string _name;
        [SerializeField] protected GameObject _weaponModel;
        [SerializeField] protected int _damageValue;
        [SerializeField] protected int _shootingDistance;
        [SerializeField] protected float _shootingSpeed;
        [SerializeField] protected int _bulletsCount;
        [SerializeField] protected float _bulletSpeed;
        [SerializeField] protected float _reloadTime;
        [SerializeField] protected int _price;

        public string Name => _name;
        public GameObject WeaponModel => _weaponModel;
        public int DamageValue => _damageValue;
        public int ShootingDistance => _shootingDistance;
        public float ShootingSpeed => _shootingSpeed;
        public int BulletsCount => _bulletsCount;
        public float BulletSpeed => _bulletSpeed;
        public float ReloadTime => _reloadTime;
        public float Price => _price;
    }
}