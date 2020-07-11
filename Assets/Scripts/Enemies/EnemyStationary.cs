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
        type = EnemyType.Stationary;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        if (Health <= 0)
        {
            DeathAnimation();
        }
    }
}
