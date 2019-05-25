using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBoth : EnemyHealth
{
	private void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Bullet(Clone)")
		{
			TakeDamage(10, col.transform.position);
		}
	}
}
