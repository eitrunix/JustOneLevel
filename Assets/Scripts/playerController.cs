using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private BoxCollider2D playerColider;

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
    private float fallMultiplier = 10.5f;
    private float lowJumpMultiplier = 10.0f;

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
    private bool isFlipped;

    // Player Damage
    private float knockBackForce = 3.0f;
    private float knockBackLength = 0.2f;
    private float knockbackCount;
    bool tookDamage;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerColider = GetComponent<BoxCollider2D>();
        currentLives = defaultLives;
        currentHealth = defaultHealth;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (knockbackCount <= 0)
        {
            Move();
        }
        else
        {
            TakeDamage();
        }
        CheckIfGrounded();
        Jump();
        FallingCode();
        CheckHealth();
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
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                sprite.flipX = true;
                isFlipped = true;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                sprite.flipX = false;
                isFlipped = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Yup");
            resetKnockBack();

        }
    }

    void TakeDamage()
    {
        currentHealth -= 0;
        if (!isFlipped)
        {
            rb2d.velocity = new Vector2(-knockBackForce, knockBackForce);
        }
        if (isFlipped)
        {
            rb2d.velocity = new Vector2(knockBackForce, knockBackForce);
        }
        knockbackCount -= Time.deltaTime;
    }

    void resetKnockBack()
    {
        knockbackCount = knockBackLength;
    }

    void CheckHealth()
    {
        if(currentHealth <= 0)
        {
            Destroy(this);
        }
    }
}
