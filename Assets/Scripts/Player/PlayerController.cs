using UnityEngine;
using Zenject;
using Configs;
using UI;
using Weapons;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerConfig _playerConfig;
        private PlayerMovement _movement;
        private Fighter _fighter;
        private Health _health;
        private UIDamageViewer _uiDamageViewer;
        private UIHealthView _uiHealthView;
        private AnimationController _animation;
        private UIWeaponView _uiWeaponView;
        private Weapon _weapon;

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
            _uiDamageViewer = GetComponent<UIDamageViewer>();
            _uiHealthView = GetComponentInChildren<UIHealthView>();
            _uiWeaponView = GetComponentInChildren<UIWeaponView>();
            _weapon = GetComponentInChildren<Weapon>();

            _health.OnDie += _animation.SetDeathAnimation;
            _health.OnDie += _fighter.OnDie;
            _health.OnDie += _movement.DeactivateMoving;
            _health.OnDie += OnDie;
            _health.OnDie += _uiWeaponView.Deactivate;
            _health.OnTakeDamage += _uiDamageViewer.DamageValueEffect;
            _health.OnHealthChanged += _uiHealthView.HealthView;

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
            
            _weapon.OnEnemyFound += _fighter.OnEnemyFounded;
            _weapon.OnStartReload += _fighter.StopAttack;
            _weapon.OnShoot += _uiWeaponView.BulletsView;
            _weapon.OnReload += _uiWeaponView.ReloadView;
        }
        
        private void Start()
        {
            _fighter.Initialize();
            _health.Initialize(_playerConfig.MaxHealth);
            _uiHealthView.Initialize(_health);
            _uiWeaponView.Initialize(_weapon);
        }
        
        private void OnDestroy()
        {
            _health.OnDie -= _animation.SetDeathAnimation;
            _health.OnDie -= _fighter.OnDie;
            _health.OnDie -= _movement.DeactivateMoving;
            _health.OnDie -= OnDie;
            _health.OnDie -= _uiWeaponView.Deactivate;
            _health.OnTakeDamage -= _uiDamageViewer.DamageValueEffect;
            _health.OnHealthChanged -= _uiHealthView.HealthView;
            
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
            
            _weapon.OnEnemyFound -= _fighter.OnEnemyFounded;
            _weapon.OnStartReload -= _fighter.StopAttack;
            _weapon.OnShoot -= _uiWeaponView.BulletsView;
            _weapon.OnReload -= _uiWeaponView.ReloadView;
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
