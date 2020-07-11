﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    Default,
    Stationary,
    Land,
    Air,

}
public class Enemy : MonoBehaviour
{
    public static EnemyType type;
    public static Rigidbody2D rb2d;
    public static Animator animator;
    public static SpriteRenderer sprite;
    private static int health;

    public static BoxCollider2D collider;

    public static int Health { get => health; set => health = value; }



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
        Destroy(this, 3);
    }


}