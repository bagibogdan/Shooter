using System;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;

        public event Action OnMovementStart;
        public event Action OnMovementStop;
    
        public bool IsMoving { get; private set; }

        private PlayerInput _playerInput;
        private Vector3 _velocity = Vector3.zero;
        private Joystick _joystick;
        private Transform _transform;
        private Rigidbody _rigidbody;
        private bool _isCanMove;
    
        [Inject]
        public void Construct(PlayerInput playerInput)
        {
            _playerInput = playerInput;
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
                OnMovementStart?.Invoke();
                IsMoving = true;
            
                _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.LookRotation(_playerInput.MovingDirection), 
                    speed * 1.5f * Time.deltaTime);
            }
            else
            {
                OnMovementStop?.Invoke();
                IsMoving = false;
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }
        }

        private void FixedUpdate()
        {
            if (!IsMoving) return;
        
            _velocity = _transform.forward * (_playerInput.MovingDirection.magnitude * speed * Time.deltaTime);
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
    }
}
