using System;
using Configs;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        public event Action OnRunMovementStart;
        public event Action OnWalkMovementStart;
        public event Action OnMovementStop;

        private const float SPEED_COEFFICIENT = 50f;
        private const float Y_ROTATION_OFFSET_0 = 0.5150381f; // Model rotation offset for firing animation
        private const float Y_ROTATION_OFFSET_180 = 62f; // Model rotation offset for firing animation
        private PlayerConfig _playerConfig;
        private PlayerInput _playerInput;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _lookingDirection = Vector3.zero;
        private Joystick _joystick;
        private Transform _transform;
        private Rigidbody _rigidbody;
        private bool _isCanMove;
        private bool _isMoving;
        private bool _isLooking;
        private Transform _target;
        private float _runMinSpeed;
    
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
            _runMinSpeed = _playerConfig.MovementSpeed / SPEED_COEFFICIENT / 2f;
            ActivateMoving();
        }

        private void Update()
        {
            if (!_isCanMove) return;
            
            if (_isLooking) LookAtObject();

            if (_playerInput.MovingDirection != Vector3.zero)
            {
                _isMoving = true;
            
                _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.LookRotation(_playerInput.MovingDirection), 
                    _playerConfig.MovementSpeed * Time.deltaTime);
            }
            else
            {
                OnMovementStop?.Invoke();
                _isMoving = false;
            }
        }

        private void FixedUpdate()
        {
            if (!_isMoving)
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
                return;
            }
        
            _velocity = _transform.forward * (_playerInput.MovingDirection.magnitude * _playerConfig.MovementSpeed * Time.fixedDeltaTime);
            _rigidbody.velocity = _velocity;

            if (Mathf.Abs(_rigidbody.velocity.x) > _runMinSpeed ||
                Mathf.Abs(_rigidbody.velocity.z) > _runMinSpeed)
            {
                OnRunMovementStart?.Invoke();
            }
            else
            {
                OnWalkMovementStart?.Invoke();
            }
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
            _isMoving = false;
            _isLooking = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
        
        public void SetLookingTarget(Transform target)
        {
            _target = target;
            _isLooking = true;
        }
    
        public void UnsetLookingTarget()
        {
            _target = null;
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
                    _playerConfig.MovementSpeed / 10 * Time.deltaTime);
            }
            else
            {
                yRotation = rotation.eulerAngles.y + Y_ROTATION_OFFSET_180;
                rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, yRotation, rotation.eulerAngles.z);
                _transform.rotation = Quaternion.Lerp(_transform.rotation, rotation,
                    _playerConfig.MovementSpeed / 10 * Time.deltaTime);
            }
        }
    }
}
