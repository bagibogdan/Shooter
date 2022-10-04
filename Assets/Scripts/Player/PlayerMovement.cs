using System;
using Configs;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        public event Action OnMovementStart;
        public event Action OnMovementStop;
        public bool IsMoving { get; private set; }
        public bool IsLooking { get; private set; }

        private const float Y_ROTATION_OFFSET = 0.5150381f;
        private PlayerConfig _playerConfig;
        private PlayerInput _playerInput;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _lookingDirection = Vector3.zero;
        private Joystick _joystick;
        private Transform _transform;
        private Rigidbody _rigidbody;
        private bool _isCanMove;
        private Transform _target;
    
        [Inject]
        public void Construct(PlayerConfig playerConfig, PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerConfig = playerConfig;
        }
    
        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            ActivateMoving();
        }

        private void Update()
        {
            if (!_isCanMove) return;
            
            if (IsLooking) LookAtObject();

            if (_playerInput.MovingDirection != Vector3.zero)
            {
                OnMovementStart?.Invoke();
                IsMoving = true;
            
                _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.LookRotation(_playerInput.MovingDirection), 
                    _playerConfig.MovementSpeed * Time.deltaTime);
            }
            else
            {
                if (!IsMoving) return;
                
                OnMovementStop?.Invoke();
                IsMoving = false;
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }
        }

        private void FixedUpdate()
        {
            if (!IsMoving) return;
        
            _velocity = _transform.forward * (_playerInput.MovingDirection.magnitude * _playerConfig.MovementSpeed * Time.fixedDeltaTime);
            _rigidbody.velocity = _velocity;
        }

        public void ActivateMoving()
        {
            _joystick = FindObjectOfType<Joystick>();
        
            if (_joystick is null)
            {
                throw new NullReferenceException(nameof(Joystick));
            }
        
            _isCanMove = true;
        }
    
        public void DeactivateMoving()
        {
            OnMovementStop?.Invoke();
            _isCanMove = false;
            IsMoving = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
        
        public void SetLookingTarget(Transform target)
        {
            _target = target;
            IsLooking = true;
        }
    
        public void UnsetLookingTarget()
        {
            _target = null;
            IsLooking = false;
        }
        
        private void LookAtObject()
        {
            if (!_target) return;
            
            _lookingDirection = (_target.position - _transform.position).normalized;
            var rotation = Quaternion.LookRotation(_lookingDirection);
            var yRotation = rotation.y + Y_ROTATION_OFFSET;
            rotation = new Quaternion(rotation.x, yRotation, rotation.z, rotation.w);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, rotation, _playerConfig.MovementSpeed / 10 * Time.deltaTime);
        }
    }
}
