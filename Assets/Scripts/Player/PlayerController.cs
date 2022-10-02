using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public bool IsMoving => _playerMovement.IsMoving;
        
        private SignalBus _signalBus;
        private PlayerMovement _playerMovement;
        private AnimationController _animation;
        private bool _isRewarded;
        
        [Inject]
        public void Construct(SignalBus signalBus, PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
            _signalBus = signalBus;
        }
        
        private void Awake()
        {
            _animation = GetComponent<AnimationController>();
            
            _signalBus.Subscribe<OnMovementStart>(_animation.SetMoveAnimation);
            _signalBus.Subscribe<OnMovementStop>(_animation.SetIdleAnimation);
        }
    
        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnMovementStart>(_animation.SetMoveAnimation);
            _signalBus.Unsubscribe<OnMovementStop>(_animation.SetIdleAnimation);
        }

        public void ActivateMoving()
        {
            _playerMovement.ActivateMoving();
        }

        public void DeactivateMoving()
        {
            _playerMovement.DeactivateMoving();
        }
    }
}
