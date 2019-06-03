using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthLaser : EnemyHealth
{
	//PlayerShooting에서 데미지판정

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
