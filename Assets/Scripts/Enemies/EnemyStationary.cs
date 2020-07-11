using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationary : Enemy
{
    int timer;
    
    public int damage = 1;
    [SerializeField]
    private int health = Health;
    private void Awake()
    {
        Health = 3;
        IsDead = false;
        Type = EnemyType.Stationary;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(rb2d.velocity.x > 0 && !IsDead || rb2d.velocity.x < 0 && !IsDead)
        {
            rb2d.velocity = new Vector2(0, 0);
        }
        if (Health <= 0)
        {
            IsDead = true;
            DeathAnimation();
        }
    }
}
