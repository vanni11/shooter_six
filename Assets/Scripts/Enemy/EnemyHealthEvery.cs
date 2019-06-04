using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthEvery : EnemyHealth
{
	private void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Bullet" && col.gameObject.GetComponentInParent<Bullet>().canAttack)
		{
			TakeDamage(20, col.transform.position);
			col.gameObject.GetComponentInParent<Bullet>().canAttack = false;
		}
		else if (col.gameObject.name == "Bullet2(Clone)" && col.gameObject.GetComponent<Bullet>().canAttack)
		{
			TakeDamage(10, col.transform.position);
			col.gameObject.GetComponent<Bullet>().canAttack = false;
		}
	}

	protected override void Death()
	{
		base.Death();
		uiManager.score += 40; //보스는 50점득점
		uiManager.SetScoreText();
		Instantiate(explodeParticle, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
		ParticleSystem[] particles = explodeParticle.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem p in particles)
		{
			p.Play();
		}
	}
}
