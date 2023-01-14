using UnityEngine;

/// <summary>
/// Temporary class to show player animations without moving the character
/// </summary>
public class PlayerController_NoMovement : MonoBehaviour
{
    public LayerMask GroundLayer;
    public bool IsGrounded;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _jumpCooldownPassed = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Update continuous states
    /// </summary>
    private void Update()
    {
        PlayJumpAnimation();
    }

    public void Move(float input)
    {
        _animator.SetFloat(Constants.AnimationPlayerMove, Mathf.Abs(input));

        if (input < 0)
        {
            _spriteRenderer.flipX = true;
        } 
        
        if (input > 0)
        {
            _spriteRenderer.flipX = false;
        } 
    }

    void PlayJumpAnimation()
    {
        _animator.SetBool(Constants.AnimationPlayerJump, !IsGrounded);
    }
    
    public void Attack()
    {
        _animator.SetTrigger(Constants.AnimationPlayerAttack);
    }
}
