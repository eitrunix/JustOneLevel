using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

	public Transform firePoint;
	public GameObject bulletPrefab;

	bool canFire = true;
	float wepCD = 0.45f;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButton("Fire1") && canFire)
		{
			canFire = !canFire;
			Shoot();
		}
	}

	void Shoot()
	{

		Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		StartCoroutine(CoolDown());
	}

	IEnumerator CoolDown()
	{
		yield return new WaitForSeconds(wepCD);
		canFire = true;
	}
}