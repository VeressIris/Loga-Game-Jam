using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    void Update()
    {
        rb.velocity = new Vector2(moveInputX * walkSpeed, rb.velocity.y);

        if (facingRight && moveInputX < 0)
        {
            Flip();
        }
        else if (!facingRight && moveInputX > 0)
        {
            Flip();
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        moveInputX = ctx.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }

        //make player jump higher the longer they hold the jump button
        if (ctx.canceled && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
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
}
