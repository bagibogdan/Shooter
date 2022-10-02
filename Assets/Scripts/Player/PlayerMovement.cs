using System;
using Configs;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        public bool IsMoving { get; private set; }
        
        private SignalBus _signalBus;
        private PlayerConfig _playerConfig;
        private PlayerInput _playerInput;
        private Vector3 _velocity = Vector3.zero;
        private Joystick _joystick;
        private Transform _transform;
        private Rigidbody _rigidbody;
        private bool _isCanMove;
    
        [Inject]
        public void Construct(SignalBus signalBus, PlayerConfig playerConfig, PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _signalBus = signalBus;
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

            if (_playerInput.MovingDirection != Vector3.zero)
            {
                _signalBus.Fire(new OnMovementStart());
                IsMoving = true;
            
                _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.LookRotation(_playerInput.MovingDirection), 
                    _playerConfig.MovementSpeed * 1.5f * Time.deltaTime);
            }
            else
            {
                _signalBus.Fire(new OnMovementStop());
                IsMoving = false;
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }
        }

        private void FixedUpdate()
        {
            if (!IsMoving) return;
        
            _velocity = _transform.forward * (_playerInput.MovingDirection.magnitude * _playerConfig.MovementSpeed * Time.deltaTime);
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
            _signalBus.Fire(new OnMovementStop());
            _isCanMove = false;
            IsMoving = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}
