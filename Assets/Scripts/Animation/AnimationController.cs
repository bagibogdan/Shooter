using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private const string IDLE = "Idle";
    private const string RUN = "Run";
    private const string WALK = "Walk";
    private const string ATTACK = "Attack";
    private const string DEATH = "Death";
    private static readonly int Idle = Animator.StringToHash(IDLE);
    private static readonly int Run = Animator.StringToHash(RUN);
    private static readonly int Walk = Animator.StringToHash(WALK);
    private static readonly int Attack = Animator.StringToHash(ATTACK);
    private static readonly int Death = Animator.StringToHash(DEATH);
    
    private Animator _animator;
    private int _currentAnimation;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _currentAnimation = Idle;
    }

    public void SetIdleAnimation()
    {
        if (_currentAnimation == Idle) return;
        
        _animator.SetTrigger(Idle);
        _currentAnimation = Idle;
    }
    
    public void SetRunMoveAnimation()
    {
        if (_currentAnimation == Run) return;
        
        _animator.SetTrigger(Run);
        _currentAnimation = Run;
    }
    
    public void SetWalkMoveAnimation()
    {
        if (_currentAnimation == Walk) return;
        
        _animator.ResetTrigger(Idle);
        _animator.SetTrigger(Walk);
        _currentAnimation = Walk;
    }
    
    public void SetAttackAnimation()
    {
        _animator.SetTrigger(Attack);
        _currentAnimation = Attack;
    }
    
    public void SetDeathAnimation()
    {
        if (_currentAnimation == Death) return;
        
        _animator.SetTrigger(Death);
        _currentAnimation = Death;
    }
}
