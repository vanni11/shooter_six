using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
	public float sinkSpeed = 2.5f; //죽은 적이 바닥으로 가라앉는 속도
	public int scoreValue = 10;
	public AudioClip deathClip; //죽으면 플레이 되는 오디오 클립

	Animator anim;
	AudioSource enemyAudio;
	ParticleSystem hitParticles;
	CapsuleCollider capsuleCollider;
	bool isDead;
	bool isSinking;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		enemyAudio = GetComponent<AudioSource>();
		hitParticles = GetComponentInChildren<ParticleSystem>();
		capsuleCollider = GetComponent<CapsuleCollider>();

		currentHealth = startingHealth;
	}

	private void Update()
	{
		if (isSinking)
		{
			transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);// 가라 앉히기
		}
	}
	
	//총알은 EnemyHealthBullet.cs에서
	//레이저는PlayerShooting.cs 에서
	public void TakeDamage(int amount, Vector3 hitPoint)
	{
		if (isDead)
		{
			return;
		}

		// enemyAudio.Play();

		currentHealth -= amount;

		hitParticles.transform.position = hitPoint;
		hitParticles.Play();

		if (currentHealth <= 0)
		{
			Death();
		}
	}

	void Death()
	{
		isDead = true;

		capsuleCollider.isTrigger = true;
		StartSinking();
		NavMeshAgent nav = GetComponent<NavMeshAgent>();
		nav.enabled = false;

		//anim.SetTrigger("Dead");
		// enemyAudio.clip = deathClip;
		//enemyAudio.Play();
	}

	public void StartSinking()
	{
		GetComponent<NavMeshAgent>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true; // 유니티가 다시 계산하지 않는다.
		isSinking = true;
		Destroy(gameObject, 2.0f);
	}
}
