using UnityEngine;
using Zenject;
using Configs;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public bool IsMoving => _movement.IsMoving;

        private PlayerConfig _playerConfig;
        private PlayerMovement _movement;
        private Fighter _fighter;
        private Health _health;
        private AnimationController _animation;
        private bool _isRewarded;
        
        [Inject]
        public void Construct(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
        }
        
        private void Awake()
        {
            _animation = GetComponent<AnimationController>();
            _movement = GetComponent<PlayerMovement>();
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();

            _fighter.Initialize();
            _health.Initialize(_playerConfig.MaxHealth);
            
            _health.OnDie += _animation.SetDeathAnimation;
            _health.OnDie += _fighter.OnDie;
            
            _fighter.OnStartFight += _animation.SetAttackAnimation;
            _fighter.OnResetFightTarget += _animation.SetIdleAnimation;
            _fighter.OnSetFightTarget += _movement.SetLookingTarget;
            _fighter.OnStopFight += _movement.UnsetLookingTarget;
            
            _movement.OnMovementStart += _animation.SetMoveAnimation;
            _movement.OnMovementStop += _animation.SetIdleAnimation;
            _movement.OnMovementStart += _fighter.ShootingDeactivate;
            _movement.OnMovementStop += _fighter.ShootingActivate;
        }
        
        private void OnDestroy()
        {
            _health.OnDie -= _animation.SetDeathAnimation;
            _health.OnDie -= _fighter.OnDie;
            
            _fighter.OnStartFight -= _animation.SetAttackAnimation;
            _fighter.OnResetFightTarget -= _animation.SetIdleAnimation;
            _fighter.OnSetFightTarget -= _movement.SetLookingTarget;
            _fighter.OnStopFight -= _movement.UnsetLookingTarget;
            
            _movement.OnMovementStart -= _animation.SetMoveAnimation;
            _movement.OnMovementStop -= _animation.SetIdleAnimation;
            _movement.OnMovementStart -= _fighter.ShootingDeactivate;
            _movement.OnMovementStop -= _fighter.ShootingActivate;
        }
        
        public void ActivateMoving()
        {
            _movement.ActivateMoving();
        }

        public void DeactivateMoving()
        {
            _movement.DeactivateMoving();
        }
    }
}
