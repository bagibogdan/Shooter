using System;
using UnityEngine;
using UnityEngine.AI;
using Configs;

namespace Enemy
{
    public abstract class EnemyMovement : MonoBehaviour
    {
        public event Action OnRunMovementStart;
        public event Action OnWalkMovementStart;
        public event Action OnMovementStop;

        public event Action OnMovementPoint;

        public bool IsMoving => _isMoving;
        
        private const float Y_ROTATION_OFFSET_0 = 0.5150381f; // Model rotation offset for firing animation
        private const float Y_ROTATION_OFFSET_180 = 62f; // Model rotation offset for firing animation
        
        protected EnemyConfig _enemyConfig;
        protected Transform _player;
        private Vector3 _lookingDirection = Vector3.zero;
        private Vector3 _movingToPoint;
        private Transform _transform;
        private Transform _target;
        private NavMeshAgent _navMeshAgent;
        private bool _isMoving;
        private bool _isLooking;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = _enemyConfig.MovementSpeed;
            _transform = transform;
        }
        
        private void Update()
        {
            if (_isMoving)
            {
                if (_movingToPoint == Vector3.zero)
                {
                    StartRunMove(_player.position);
                    return;
                }
                
                if (transform.position.x == _movingToPoint.x && transform.position.z == _movingToPoint.z)
                {
                    OnMovementStop?.Invoke();
                    _isMoving = false;
                    OnMovementPoint?.Invoke();
                }
                return;
            }
            
            if (_isLooking) LookAtObject();

            if (_navMeshAgent.isStopped)
            {
                _isMoving = false;
                OnMovementStop?.Invoke();
            }
        }
        
        public void StartWalkMove(Vector3 movingPoint)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.speed = _enemyConfig.MovementSpeed / 5f;
            _movingToPoint = movingPoint;
            MoveToPoint(movingPoint);
            OnWalkMovementStart?.Invoke();
        }        
        
        public void StartRunMove(Vector3 movingPoint)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.speed = _enemyConfig.MovementSpeed;
            _movingToPoint = Vector3.zero;
            MoveToPoint(movingPoint);
            OnRunMovementStart?.Invoke();
        }
        
        private void MoveToPoint(Vector3 targetPoint)
        {
            _isMoving = true;
            _navMeshAgent.SetDestination(targetPoint);
        }

        public void StopMoving()
        {
            _navMeshAgent.isStopped = true;
            _isMoving = false;
            OnMovementStop?.Invoke();
        }

        public void Reset()
        {
            _isMoving = false;
            _isLooking = false;            
        }

        public void SetLookingTarget(Transform target)
        {
            if (!_target) _target = target;
            _isLooking = true;
        }
    
        public void UnsetLookingTarget()
        {
            _isLooking = false;
        }
        
        private void LookAtObject()
        {
            if (!_target) return;
            
            _lookingDirection = (_target.position - _transform.position).normalized;
            var rotation = Quaternion.LookRotation(_lookingDirection);
            float yRotation;

            if (rotation.eulerAngles.y < 180f)
            {
                yRotation = rotation.y + Y_ROTATION_OFFSET_0;
                rotation = new Quaternion(rotation.x, yRotation, rotation.z, rotation.w);
                _transform.rotation = Quaternion.Lerp(_transform.rotation, rotation,
                    _enemyConfig.MovementSpeed / 10 * Time.deltaTime);
            }
            else
            {
                yRotation = rotation.eulerAngles.y + Y_ROTATION_OFFSET_180;
                rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, yRotation, rotation.eulerAngles.z);
                _transform.rotation = Quaternion.Lerp(_transform.rotation, rotation,
                    _enemyConfig.MovementSpeed / 10 * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isMoving) return;
            
            if (other.gameObject.transform.position == _movingToPoint)
            {
                OnMovementStop?.Invoke();
                other.gameObject.SetActive(false);
                _isMoving = false;
                OnMovementPoint?.Invoke();
            }
        }
    }
}
