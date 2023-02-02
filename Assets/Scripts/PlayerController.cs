using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float SpeedModifier = 1f;
    public float JumpModifier = 1f;
    public float JumpCooldown = 0.2f;
    public float InAirCooldown = 0.1f;
    public LayerMask GroundLayer;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private bool _isGrounded;
    private bool _jumpCooldownPassed = true;

    private Transform _parentTransform;
    private Rigidbody2D _parentRigidbody;   

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Update continuous states
    /// </summary>
    private void Update()
    {
        bool newGroundedState = CheckGroundedState();

        // if player is just landed, then start cooldown
        if (!_isGrounded && newGroundedState)
        {
            _jumpCooldownPassed = false;
            StartCoroutine(nameof(PerformJumpCooldown));
        }
        
        _isGrounded = newGroundedState;

        PlayJumpAnimation();
    }

    public void Move(float input)
    {
        if (input == 0) return;
        CheckParent();
        
        Vector2 currentVeclocity = _rigidbody.velocity;
        Vector2 parentVelocity = _parentRigidbody ? _parentRigidbody.velocity : Vector2.zero;
        
        _animator.SetFloat(Constants.AnimationPlayerMove, Mathf.Abs(input));
        _rigidbody.velocity = parentVelocity + new Vector2(input * SpeedModifier, currentVeclocity.y);

        SetLookDirection(input);
    }

    void SetLookDirection(float input)
    {
        if (input < 0)
        {
            _spriteRenderer.flipX = true;
        } 
        
        if (input > 0)
        {
            _spriteRenderer.flipX = false;
        } 
    }

    void CheckParent()
    {
        _parentTransform = gameObject.transform.parent;
        if (_parentTransform && !_parentRigidbody)
        {
            _parentRigidbody = _parentTransform.GetComponentInParent<Rigidbody2D>();
        }

        if (!_parentTransform)
        {
            _parentRigidbody = null;
        }
    }

    /// <summary>
    /// Perform jump if all conditions passed
    /// </summary>
    /// <param name="input"></param>
    public void Jump(float input)
    {
        if (input > 0 && _isGrounded && _jumpCooldownPassed)
        {
            _rigidbody.AddForce(JumpModifier * Vector2.up, ForceMode2D.Impulse);
            _isGrounded = false;
        }
    }

    void PlayJumpAnimation()
    {
        _animator.SetBool(Constants.AnimationPlayerJump, !_isGrounded);
    }

    /// <summary>
    /// To check if player is grounded we'll cast a ray a little further than
    /// the collider size
    /// </summary>
    /// <returns>If the player is grounded</returns>
    private bool CheckGroundedState()
    {
        Vector2 originPos = (Vector2)transform.position + Vector2.up * 0.005f;
        Vector2 ray = Vector2.down;
        float distance = 0.01f;
        
        RaycastHit2D hit = Physics2D.Raycast(originPos, ray, distance, GroundLayer);

        bool newIsGrounded = hit.collider;

        // if (_isGrounded && !newIsGrounded)
        // {
        //     StopCoroutine();
        //     return true;
        // }

        return newIsGrounded;
    }
    
    public void Attack()
    {
        _animator.SetTrigger(Constants.AnimationPlayerAttack);
    }

    /// <summary>
    /// Wait timeout to reset cooldown
    /// </summary>
    /// <returns></returns>
    public IEnumerator PerformJumpCooldown()
    {
        yield return new WaitForSeconds(JumpCooldown);
        _jumpCooldownPassed = true;
    }

    /// <summary>
    /// Wait timeout to reset grounded state
    /// </summary>
    /// <returns></returns>
    public IEnumerator PerformInAirCooldown()
    {
        yield return new WaitForSeconds(InAirCooldown);
        _isGrounded = false;
    }
}
