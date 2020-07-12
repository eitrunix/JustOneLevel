using System.Collections;
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
    private static EnemyType type;
    private static bool isDead;

	public int scoreAdded;
	public int health;
	public int damage;
	public static bool IsDead { get => isDead; set => isDead = value; }
    public static EnemyType Type { get => type; set => type = value; }

	public virtual void TakeDamage(int _damage) {
		health -= _damage;

		if (health <= 0)
		{
			Die();
		}
	}
	public virtual int GiveDamage()
	{
		return damage;
	}
	public virtual void Die()
	{
		UI tmpUI = FindObjectOfType<UI>();
		GameManager.instance.AddScore(scoreAdded);
		Destroy(gameObject);
	}

	private void Update()
	{
		if(GameManager.instance.IsEndLevel)
		{
			endLevelDie();
		}
	}
	private void endLevelDie()
	{
		Destroy(gameObject);

	}
	public virtual int RecievePoints()
	{
		return scoreAdded;
	}
}
