using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public float speed = 20f;
	public int damage;
	public Rigidbody2D rb;
	private float bulletLifetime = 0.5f;
	//public GameObject impactEffect;

	// Use this for initialization
	void Start()
	{
		rb.velocity = transform.right * speed;
		Debug.Log("Bullet Made");
	}

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		if (hitInfo.gameObject.CompareTag("Enemy") || hitInfo.gameObject.CompareTag("Ground"))
		{
			Enemy enemy = hitInfo.GetComponent<Enemy>();
			if (enemy != null)
			{
				enemy.TakeDamage(damage);
			}

			//Instantiate(impactEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		Debug.Log(bulletLifetime);
		bulletLifetime -= Time.deltaTime;
		if(bulletLifetime <= 0)
		{
			Destroy(gameObject);
		}	
	}
}