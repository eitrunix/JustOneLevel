﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public static playerController instance = null; 

    private BoxCollider2D playerColider;

    private SpriteRenderer sprite;
    private Rigidbody2D rb2d;
    // Movespeed
    [Range(0.0f, 50.0f)]
    public float speed;

    // Jumping
    [Range(0.0f, 20.0f)]
    public float jumpForce;
    [Range(0.0f, 15.0f)]
    public float fallMultiplier = 10.5f;  // private
    [Range(0.0f, 10.0f)]
    public float lowJumpMultiplier = 10.0f; // private

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
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerColider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsPlaying && !GameManager.instance.IsStartLevel)
        {
            if (knockbackCount <= 0)
            {
                Move();
            }
            else
            {
                TakeDamage(0);
            }
            CheckIfGrounded();
            Jump();
            FallingCode();
        }
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
        if (Input.GetAxisRaw("Horizontal") < 0 && !isFlipped)
        {
            //sprite.flipX = true;
            flip();
        }
        if (Input.GetAxisRaw("Horizontal") > 0 && isFlipped)
        {
            //sprite.flipX = false;
            flip();
        }

    }

    void flip()
    {
        isFlipped = !isFlipped;
        transform.Rotate(0f, 180f, 0f);
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
        if (rb2d.velocity.y < 0 || rb2d.velocity.y > 0 && tookDamage)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.CompareTag("LevelFlag"))
        //{

        //    SceneManager.LoadScene("Level1");
        //}

        Enemy enemy;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy = collision.gameObject.GetComponent<Enemy>();
            TakeDamage(enemy.GiveDamage());
            resetKnockBack();

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    void TakeDamage(int dmg)
    {
        GameManager.instance.CurrentHealth -= dmg;
        tookDamage = true;
        StartCoroutine(resetDamageCD());

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

    IEnumerator resetDamageCD()
    {
        yield return new WaitForSeconds(0.4f);
        tookDamage = false;

    }
    void resetKnockBack()
    {
        knockbackCount = knockBackLength;
    }

}
