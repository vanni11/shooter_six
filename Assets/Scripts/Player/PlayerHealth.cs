using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	PlayerSwitch playerSwitch;
	PlayerController playerController;
	PlayerShooting playerShooting;
	public RifleColorChanger[] rifleColorChanger = new RifleColorChanger[2]; //꺼져있는 오브젝트에서 가져와야해서 직접참조시킴

	public int startingHealth = 100; // 처음 체력
	public int currentHealth; // 현재 체력
	public Slider healthSlider; // 체력 바
	public Image damageImage; // 데미지를 입었을때 이미지
	public float flashSpeed = 5.0f; // 데미지 입었을 때 깜빡이는 속도
	public Color flashColor = new Color(1.0f, 0.0f, 0.0f, 0.1f);

	public Animator anim;
	//쏘는 사운드와 맞는 사운드 별개로 해야해서 오브젝트 따로만듬
	public AudioSource audioSource; //맞고 죽는용
	public AudioClip damagedClip; //맞을때 소리
	public AudioClip deathClip; // 죽었을때 소리

	public bool isDead; // 죽었나 확인
	bool damaged; // 데미지를 입었나 확인

	UIManager uIManager;

	private void Awake()
	{
		playerSwitch = GetComponent<PlayerSwitch>();
		playerController = GetComponent<PlayerController>();
		playerShooting = GetComponent<PlayerShooting>();

		currentHealth = startingHealth;

		anim = GetComponentInChildren<Animator>();
		//playerAudio = GetComponent<AudioSource>();

		uIManager = FindObjectOfType<UIManager>();

		audioSource = GetComponentInChildren<AudioSource>(); 
	}

	private void Update()
	{
		if (damaged)
		{
			damageImage.color = flashColor;
		}
		else
		{
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	public void TakeDamage(int amount)
	{
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;

		if (currentHealth <= 0 && !isDead)
		{
			Death();
		}
		
		audioSource.PlayOneShot(damagedClip);
	}

	public void Death()
	{
		isDead = true;

		anim.SetTrigger("Die");
		audioSource.PlayOneShot(deathClip);

		playerShooting.RemoveLaser(); //노랑레이저 쏘면서 죽으면 남아서..
		playerSwitch.enabled = false;
		playerController.enabled = false;
		playerShooting.enabled = false;
		rifleColorChanger[0].enabled = false;
		rifleColorChanger[1].enabled = false;

		StartCoroutine(uIManager.Gameover());
	}
}
