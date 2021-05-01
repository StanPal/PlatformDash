using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event System.Action OnLand;
    public event System.Action OnJump;

    [SerializeField]
    float moveSpeed = 10f;

    [SerializeField]
    float jumpForce = 50f;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    int doubleJumps = 1;

    private Vector2 groundCheckSize = Vector2.one;

    private Rigidbody2D rb;

    private Vector2 velocity = Vector2.zero;
    private float movementSmoothing = 0.01f;
    private float moveDirection;
    private bool grounded = false;
    private int jumps = 0;
    private bool mInvert;
    public bool Inverted { get { return mInvert; } set { mInvert = value; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerStats playerStats = GetComponent<PlayerStats>();
        playerStats.OnStomp += Jump;
    }

    private void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = false;
        
        if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundMask))
        {
            grounded = true;
            jumps = 0;
            if (!wasGrounded)
                OnLand?.Invoke();
        }

        if (!Inverted)
        {
            moveDirection = Input.GetAxis("Horizontal");
        }
        else
        {
            moveDirection = Input.GetAxis("Vertical");
        }
        Vector2 targetVelocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

        if ((grounded || jumps < doubleJumps) && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    public void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce));
        jumps++;
        OnJump?.Invoke();
    }

    public void IncreaseSpeed()
    {
        moveSpeed++;
    }

    public void Slow()
    {
        if(moveSpeed > 0)
        moveSpeed--;
    }
}
