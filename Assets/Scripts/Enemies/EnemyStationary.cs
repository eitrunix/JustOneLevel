﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationary : Enemy
{
    private Rigidbody2D rb2d;
    private Animator animator;
    private SpriteRenderer sprite;
    private static BoxCollider2D collider;

    private void Awake()
    {
        Setup();
    }
    private void Setup()
    {
        IsDead = false;
        Type = EnemyType.Stationary;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (rb2d.velocity.x > 0 && !IsDead || rb2d.velocity.x < 0 && !IsDead)
        {
            rb2d.velocity = new Vector2(0, 0);
        }

        if (health <= 0)
        {
            IsDead = true;
            DeathAnimation();
        }

    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    public override int GiveDamage()
    {
        return base.GiveDamage();
    }

    public override int RecievePoints()
    {
        return base.RecievePoints();
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
}
