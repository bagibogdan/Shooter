using System;
using UnityEngine;

namespace Weapons
{
    public class Bullet : MonoBehaviour, IPoolInitializable
    {
        private Pool _pool;
        private Transform _transform;
        private Rigidbody _rigidbody;
        private float _speed;
        private int _damage;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _direction = Vector3.zero;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Initialize(int damage, float speed, Vector3 direction)
        {
            if (speed < 0 || damage < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            _speed = speed;
            _damage = damage;
            _direction = direction;
        }
        
        private void FixedUpdate()
        {
            if (_speed == 0) return;
            
            
            _velocity = _direction.normalized * (_direction.magnitude * _speed * Time.fixedDeltaTime);
            _rigidbody.velocity = _velocity;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage(_damage);
            }

            ReturnToPool();
        }

        public void PoolInitialize(Pool pool)
        {
            _pool = pool;
        }

        public void ReturnToPool()
        {
            _pool.ReturnObject(gameObject);
        }
    }
}