using UnityEngine;
using Zenject;
using Configs;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public Fighter PlayerFighter => _fighter;
        
        private PlayerConfig _playerConfig;
        private PlayerMovement _movement;
        private Fighter _fighter;
        private Health _health;
        private AnimationController _animation;

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
            _health.OnDie += _movement.DeactivateMoving;
            _health.OnDie += OnDie;

            _fighter.OnStartFight += _animation.SetAttackAnimation;
            _fighter.OnResetFightTarget += _movement.UnsetLookingTarget;
            _fighter.OnSetFightTarget += _movement.SetLookingTarget;
            _fighter.OnStopFight += _animation.SetIdleAnimation;
            
            _movement.OnRunMovementStart += _animation.SetRunMoveAnimation;
            _movement.OnWalkMovementStart += _animation.SetWalkMoveAnimation;
            _movement.OnMovementStop += _animation.SetIdleAnimation;
            _movement.OnRunMovementStart += _fighter.ShootingDeactivate;
            _movement.OnWalkMovementStart += _fighter.ShootingDeactivate;
            _movement.OnMovementStop += _fighter.ShootingActivate;
        }
        
        private void OnDestroy()
        {
            _health.OnDie -= _animation.SetDeathAnimation;
            _health.OnDie -= _fighter.OnDie;
            _health.OnDie -= _movement.DeactivateMoving;
            _health.OnDie -= OnDie;
            
            _fighter.OnStartFight -= _animation.SetAttackAnimation;
            _fighter.OnResetFightTarget -= _movement.UnsetLookingTarget;
            _fighter.OnSetFightTarget -= _movement.SetLookingTarget;
            _fighter.OnStopFight -= _animation.SetIdleAnimation;
            
            _movement.OnRunMovementStart -= _animation.SetRunMoveAnimation;
            _movement.OnWalkMovementStart -= _animation.SetWalkMoveAnimation;
            _movement.OnMovementStop -= _animation.SetIdleAnimation;
            _movement.OnRunMovementStart -= _fighter.ShootingDeactivate;
            _movement.OnRunMovementStart -= _fighter.ShootingDeactivate;
            _movement.OnWalkMovementStart -= _fighter.ShootingDeactivate;
            _movement.OnMovementStop -= _fighter.ShootingActivate;
        }

        private void OnDie()
        {
            GetComponent<Collider>().enabled = false;
            GetComponentInChildren<Animator>().applyRootMotion = true;
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
