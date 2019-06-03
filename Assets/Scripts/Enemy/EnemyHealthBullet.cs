using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBullet : EnemyHealth
{
	private void OnCollisionEnter(Collision col)
	{
		if (gameObject.name == "EnemyBullet(Clone)")
		{
			if (col.gameObject.name == "Bullet" && col.gameObject.GetComponentInParent<Bullet>().canAttack) //bullet부모에 transform만있는 오브젝트 추가해서 이렇게바꿈
			{
				TakeDamage(20, col.transform.position);
				col.gameObject.GetComponentInParent<Bullet>().canAttack = false;
			}
		}
		else if(gameObject.name == "EnemyBullet2(Clone)")
		{
			if (col.gameObject.name == "Bullet2(Clone)" && col.gameObject.GetComponent<Bullet>().canAttack)
			{
				TakeDamage(10, col.transform.position);
				col.gameObject.GetComponent<Bullet>().canAttack = false;
			}
		}
	}

	protected override void Death()
	{
		base.Death();
		Instantiate(explodeParticle, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
		ParticleSystem[] particles = explodeParticle.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem p in particles)
		{
			p.Play();
		}
	}
}
