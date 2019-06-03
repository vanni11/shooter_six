using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;

	bool isDead;
	public float sinkSpeed; //죽은 적이 바닥으로 가라앉는 속도
	bool isSinking;
	public float sinkingStartTime; //애니매이션 끝나고 가라않도록 함, 적마다 다르게 적용해서 어색하지 않게
	
	ParticleSystem hitParticles;
	CapsuleCollider capsuleCollider;
	
	Animator anim;
	public AudioClip deathClip; //죽으면 플레이 되는 오디오 클립
	AudioSource enemyAudio;

	UIManager uiManager;

	private void Awake()
	{
		currentHealth = startingHealth;

		hitParticles = GetComponentInChildren<ParticleSystem>();
		capsuleCollider = GetComponent<CapsuleCollider>();
		anim = GetComponent<Animator>();
		enemyAudio = GetComponent<AudioSource>();

		uiManager = FindObjectOfType<UIManager>();
	}
	
	//총알은 EnemyHealthBullet.cs에서
	//레이저는PlayerShooting.cs 에서
	public void TakeDamage(int amount, Vector3 hitPoint)
	{
		if (isDead)
		{
			return;
		}

		//enemyAudio.Play();

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

		anim.SetBool("Die", true);
		//enemyAudio.clip = deathClip;
		//enemyAudio.Play();

		uiManager.score += 10;
		uiManager.SetScoreText();
	}

	public void StartSinking()
	{
		GetComponent<NavMeshAgent>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true; // 유니티가 다시 계산하지 않는다.
		StartCoroutine(Sinking());
		Destroy(gameObject, 4.0f);
	}

	IEnumerator Sinking()
	{
		yield return new WaitForSeconds(0.5f);
		while (gameObject.transform.position.y > -8f)
		{
			transform.Translate(-Vector3.up * sinkSpeed * 0.05f);// 가라 앉히기
			yield return new WaitForSeconds(0.03f);
		}
	}
}
