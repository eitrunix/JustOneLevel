using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAir : Enemy
{
    private Rigidbody2D rb2d;
    private Animator animator;
    private SpriteRenderer sprite;
    private static BoxCollider2D collider;
    private float moveTime;
    private bool isFlipped;
    private void Awake()
    {
        Setup();
    }
    private void Setup()
    {
        IsDead = false;
        Type = EnemyType.Land;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Movement();
        if (health <= 0)
        {
            IsDead = true;
            DeathAnimation();
        }
    }

    public override int RecievePoints()
    {
        return base.RecievePoints();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }
    public override int GiveDamage()
    {
        return base.GiveDamage();
    }

    public void DeathAnimation()
    {
        Destroy(collider);

        StartCoroutine(DeathAnim());
    }

    public IEnumerator DeathAnim()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y + .4f);
        yield return new WaitForSeconds(0.1f);
        rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y - 2);
        Die();
    }

    public void Movement()
    {

        if (!IsDead && GameManager.instance.IsPlaying)
        {

            if (!isFlipped)
            {
                //sprite.flipX = true;
                moveTime -= Time.deltaTime;
                rb2d.velocity = new Vector2(rb2d.velocity.x, 3);

                if (moveTime <= 0)
                {
                    isFlipped = true;
                    moveTime = 1.5f;

                }
            }
            if (isFlipped)
            {

                moveTime -= Time.deltaTime;

                rb2d.velocity = new Vector2(rb2d.velocity.x, -3);

                if (moveTime <= 0)
                {
                    isFlipped = false;
                    moveTime = 1.5f;

                }

            }

        }
    }
}
