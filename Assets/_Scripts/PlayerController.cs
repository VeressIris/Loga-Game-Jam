using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Walking:")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    private float moveInputX;
    private bool facingRight = true;

    [Header("Jumping:")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float jumpStrength = 10f;
    [SerializeField] private float doubleJumpStrength = 6.5f;
    [SerializeField] bool canDoubleJump = false;
    private bool isDoubleJumping = false;
    private bool isJumping = false;

    [Header("Health:")]
    public int health = 3;

    [Header("Dashing:")]
    [SerializeField] private bool canDash = false;
    [SerializeField] private float dashStrength = 2f;
    [SerializeField] private float dashDuration = 0.25f;
    [SerializeField] private float dashCooldown = 1.5f;
    private bool isDashing = false;
    private float originalGravity;

    [Header("Animation:")]
    public Animator anim;
    [SerializeField] private PlayerInput playerInput;

    void Start()
    {
        health = PlayerPrefs.GetInt("PlayerHealth");

        playerInput.enabled = true;

        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    void Update()
    {
        rb.velocity = new Vector2(moveInputX * walkSpeed, rb.velocity.y);

        if (IsGrounded())
        {
            isDoubleJumping = false;
            isJumping = false;
        }

        Animate();

        if (health == 0)
        {
            StartCoroutine(KillPlayer());
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        anim.Play("walk");
        moveInputX = ctx.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (IsGrounded())
            {
                isJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
            }
            else if (!IsGrounded() && canDoubleJump && !isDoubleJumping)
            {
                anim.Play("jump");
                rb.velocity = new Vector2(rb.velocity.x, doubleJumpStrength);
                
                isDoubleJumping = true;
            }
        }

        //make player jump higher the longer they hold the jump button
        if (ctx.canceled && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Dash()
    {
        if (canDash && !isDashing)
        {
            //anim.Play("dash");

            isDashing = true;
            canDash = false;

            originalGravity = rb.gravityScale;
            rb.gravityScale = 0;

            rb.AddForce(transform.right * dashStrength * transform.localScale.x, ForceMode2D.Force);

            StartCoroutine(StopDash());
        }
    }

    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //make sure sprite is facing the direction it's going
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; //flip sprite
        transform.localScale = localScale;
    }

    private void Animate()
    {
        if (facingRight && moveInputX < 0) Flip();
        else if (!facingRight && moveInputX > 0) Flip();

        if (health > 0)
        {
            if (IsIdle()) anim.Play("Idle");
            if (!IsGrounded() && !isDashing) anim.Play("jump");
            if (isDashing) anim.Play("dash"); 
        }
    }

    private bool IsIdle()
    {
        return moveInputX == 0 && IsGrounded() && !isJumping && !isDoubleJumping && !isDashing;
    }

    private IEnumerator KillPlayer()
    {
        anim.Play("death");
        rb.bodyType = RigidbodyType2D.Static;
        playerInput.enabled = false;

        yield return new WaitForSeconds(2f);

        //restart game
        PlayerPrefs.SetInt("PlayerHealth", 3); //give player the health back
        SceneManager.LoadScene(1);
    }
}
