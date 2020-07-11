using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public SpriteRenderer bg;

    private SpriteRenderer sprite;
    private Rigidbody2D rb2d;
    // Movespeed
    public float speed;

    // Health
    private static int defaultHealth = 3;
    private static int defaultLives = 3;

    public int currentLives;
    public int currentHealth;

    // Jumping
    public float jumpForce;
    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2f;

    // Double Jump
    public int defaultAdditionalJumps = 0;
    int additionalJumps;
    
    // Ground Check
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public float rememberGroundedFor;
    float lastTimeGrounded;

    // Animations
    private Animator animator;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        currentLives = defaultLives;
        currentHealth = defaultHealth;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckIfGrounded();
        Jump();
        FallingCode();
        
    }

    void Move()
    {
        animator.SetBool("isRunning", true);

        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb2d.velocity = new Vector2(moveBy, rb2d.velocity.y);
        
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            sprite.flipX = true;
        }       
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            sprite.flipX = false;
        }

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor || additionalJumps > 0))
        {
            animator.SetBool("isJumping", true);
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            additionalJumps--;
        }
    }

    void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (colliders != null)
        {
            animator.SetBool("isJumping", false);
            isGrounded = true;
            additionalJumps = defaultAdditionalJumps;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }

    void FallingCode()
    {
        if (rb2d.velocity.y < 0)
        {
            animator.SetBool("isJumping", true);
            rb2d.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("isJumping", true);
            rb2d.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
