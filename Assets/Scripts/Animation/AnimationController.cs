using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private const string IDLE = "Idle";
    private const string MOVE = "Move";
    private const string ATTACK = "Attack";
    private const string DEATH = "Death";
    private static readonly int Idle = Animator.StringToHash(IDLE);
    private static readonly int Move = Animator.StringToHash(MOVE);
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
    
    public void SetMoveAnimation()
    {
        if (_currentAnimation == Move) return;
        
        _animator.SetTrigger(Move);
        _currentAnimation = Move;
    }
    
    public void SetAttackAnimation()
    {
        if (_currentAnimation == Attack) return;
        
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
