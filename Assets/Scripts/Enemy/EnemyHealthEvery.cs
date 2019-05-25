using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthEvery : EnemyHealth
{
	private void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Bullet(Clone)" && col.gameObject.GetComponent<Bullet>().canAttack)
		{
			TakeDamage(20, col.transform.position);
			col.gameObject.GetComponent<Bullet>().canAttack = false;
		}
		else if(col.gameObject.name == "Bullet2(Clone)" && col.gameObject.GetComponent<Bullet>().canAttack)
		{
			TakeDamage(10, col.transform.position);
			col.gameObject.GetComponent<Bullet>().canAttack = false;
		}
	}
}
