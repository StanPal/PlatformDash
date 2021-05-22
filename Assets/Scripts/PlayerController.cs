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
    private BoxCollider2D _boxCollider2D; 
    private Rigidbody2D rb;

    private Vector2 velocity = Vector2.zero;
    private float movementSmoothing = 0.01f;
    private float moveDirection;
    private bool grounded = false;
    private int jumps = 2;
    private bool mInvert;
    public bool Inverted { get { return mInvert; } set { mInvert = value; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerStats playerStats = GetComponent<PlayerStats>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        playerStats.OnStomp += Jump;
    }

    private void FixedUpdate()
    {
        //bool wasGrounded = grounded;
        //grounded = false;
        
        //if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundMask))
        //{
        //    grounded = true;
        //    jumps = 0;
        //    if (!wasGrounded)
        //        OnLand?.Invoke();
        //}

        if(IsGrounded())
        {            
            jumps = 1;
        }
        else
        {
            OnLand?.Invoke();
        }

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if(Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else
        {
            if(Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(+moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
                
        }

        //if (!Inverted)
        //{
        //    moveDirection = Input.GetAxis("Horizontal");
        //}
        //else
        //{
        //    moveDirection = Input.GetAxis("Vertical");
        //}
        //Vector2 targetVelocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        //rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);        
    }

    private void Update()
    {
        if ((IsGrounded() || jumps > 0) && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public bool IsGrounded()
    {
        float extraheight = 0.5f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(_boxCollider2D.bounds.center, Vector2.down, _boxCollider2D.bounds.extents.y + extraheight,groundMask);
        Color rayColor; 
        if(raycastHit2D.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(_boxCollider2D.bounds.center, Vector2.down * (_boxCollider2D.bounds.extents.y + _boxCollider2D.bounds.extents.x), rayColor);
        return raycastHit2D.collider != null; 
    }

    public void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        jumps--;
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
