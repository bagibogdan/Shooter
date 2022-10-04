using UnityEngine;
using Zenject;
using Configs;
using Level;

namespace Enemy
{
    public abstract class EnemyController : MonoBehaviour
    {
        protected EnemyConfig _enemyConfig;
        protected SignalBus _signalBus;
        private EnemyMovement _movement;
        protected Fighter _playerFighter;
        private Fighter _fighter;
        private Health _health;
        private AnimationController _animation;
        protected LevelController _levelController;

        private void Awake()
        {
            _animation = GetComponent<AnimationController>();
            _movement = GetComponent<EnemyMovement>();
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();

            _health.OnDie += _animation.SetDeathAnimation;
            _health.OnDie += _fighter.OnDie;
            _health.OnDie += _movement.Reset;
            _health.OnDie += OnDie;

            _fighter.OnStartFight += _animation.SetAttackAnimation;
            _fighter.OnResetFightTarget += _movement.UnsetLookingTarget;
            _fighter.OnSetFightTarget += _movement.SetLookingTarget;
            _fighter.OnStopFight += _animation.SetIdleAnimation;
            _fighter.OnEnemyFound += _movement.StopMoving;
            
            _movement.OnRunMovementStart += _animation.SetRunMoveAnimation;
            _movement.OnWalkMovementStart += _animation.SetWalkMoveAnimation;
            _movement.OnMovementStop += _animation.SetIdleAnimation;
            _movement.OnRunMovementStart += _fighter.ShootingDeactivate;
            _movement.OnWalkMovementStart += _fighter.ShootingDeactivate;
            _movement.OnMovementStop += _fighter.ShootingActivate;
        }

        private void Start()
        {
            _fighter.Initialize(_playerFighter);
            _health.Initialize(_enemyConfig.MaxHealth);
            _movement.StartWalkMove(_levelController.GetMovementPoint());
        }

        private void Update()
        {
            if (_fighter.CurrentEnemy != null && !_fighter.IsFight && !_movement.IsMoving)
            {
                _movement.StartRunMove(_playerFighter.gameObject.transform);
            }
        }
        
        private void OnDestroy()
        {
            _health.OnDie -= _animation.SetDeathAnimation;
            _health.OnDie -= _fighter.OnDie;
            _health.OnDie -= _movement.Reset;
            _health.OnDie -= OnDie;
            
            _fighter.OnStartFight -= _animation.SetAttackAnimation;
            _fighter.OnResetFightTarget -= _movement.UnsetLookingTarget;
            _fighter.OnSetFightTarget -= _movement.SetLookingTarget;
            _fighter.OnStopFight -= _animation.SetIdleAnimation;
            _fighter.OnEnemyFound -= _movement.StopMoving;
            
            _movement.OnRunMovementStart -= _animation.SetRunMoveAnimation;
            _movement.OnRunMovementStart -= _animation.SetWalkMoveAnimation;
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
    }
}
