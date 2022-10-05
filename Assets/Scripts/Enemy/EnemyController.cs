using UnityEngine;
using Zenject;
using Configs;
using Level;
using UI;
using Weapons;

namespace Enemy
{
    public abstract class EnemyController : MonoBehaviour
    {
        protected SignalBus _signalBus;
        protected EnemyConfig _enemyConfig;
        protected Fighter _playerFighter;
        protected LevelController _levelController;
        private EnemyMovement _movement;
        private Fighter _fighter;
        private Health _health;
        private UIDamageViewer _uiDamageViewer;
        private UIHealthView _uiHealthView;
        private AnimationController _animation;
        private UIWeaponView _uiWeaponView;
        private Weapon _weapon;

        private void Awake()
        {
            _animation = GetComponent<AnimationController>();
            _movement = GetComponent<EnemyMovement>();
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _uiDamageViewer = GetComponent<UIDamageViewer>();
            _uiHealthView = GetComponentInChildren<UIHealthView>();
            _uiWeaponView = GetComponentInChildren<UIWeaponView>();
            _weapon = GetComponentInChildren<Weapon>();

            _health.OnDie += _animation.SetDeathAnimation;
            _health.OnDie += _fighter.OnDie;
            _health.OnDie += _movement.Reset;
            _health.OnDie += OnDie;
            _health.OnDie += _uiWeaponView.Deactivate;
            _health.OnTakeDamage += _uiDamageViewer.DamageValueEffect;
            _health.OnDamageDetected += _fighter.OnEnemyFoundedOnShoot;
            _health.OnHealthChanged += _uiHealthView.HealthView;

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
            _movement.OnMovementPoint += WalkToNewPoint;
            
            _weapon.OnEnemyFound += _fighter.OnEnemyFounded;
            _weapon.OnStartReload += _fighter.StopAttack;
            _weapon.OnShoot += _uiWeaponView.BulletsView;
            _weapon.OnReload += _uiWeaponView.ReloadView;
        }

        private void Start()
        {
            _fighter.Initialize(_playerFighter);
            _health.Initialize(_enemyConfig.MaxHealth);
            _uiHealthView.Initialize(_health);
            _uiWeaponView.Initialize(_weapon);
            _movement.StartWalkMove(_levelController.GetMovementPoint());
        }

        private void Update()
        {
            if (_fighter.CurrentEnemy != null && !_fighter.IsFight && !_movement.IsMoving)
            {
                _movement.StartRunMove(_playerFighter.gameObject.transform.position);
            }
        }
        
        private void OnDestroy()
        {
            _health.OnDie -= _animation.SetDeathAnimation;
            _health.OnDie -= _fighter.OnDie;
            _health.OnDie -= _movement.Reset;
            _health.OnDie -= OnDie;
            _health.OnDie -= _uiWeaponView.Deactivate;
            _health.OnTakeDamage -= _uiDamageViewer.DamageValueEffect;
            _health.OnDamageDetected -= _fighter.OnEnemyFoundedOnShoot;
            _health.OnHealthChanged -= _uiHealthView.HealthView;
            
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
            _movement.OnMovementPoint -= WalkToNewPoint;
            
            _weapon.OnEnemyFound -= _fighter.OnEnemyFounded;
            _weapon.OnStartReload -= _fighter.StopAttack;
            _weapon.OnShoot -= _uiWeaponView.BulletsView;
            _weapon.OnReload -= _uiWeaponView.ReloadView;
        }

        private void WalkToNewPoint()
        {
            _movement.StartWalkMove(_levelController.GetMovementPoint());
        }
        
        private void OnDie()
        {
            GetComponent<Collider>().enabled = false;
        }
    }
}
