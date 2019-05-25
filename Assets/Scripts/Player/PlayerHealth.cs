using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100; // 처음 체력
    public int currentHealth; // 현재 체력
    public Slider healthSlider; // 체력 바
    public Image damageImage; // 데미지를 입었을때 이미지
    public AudioClip deathClip; // 죽었을때 소리
    public float flashSpeed = 5.0f; // 데미지 입었을 때 깜빡이는 속도?
    public Color flashColor = new Color(1.0f, 0.0f, 0.0f, 0.1f);

    //Animator anim;
    //AudioSource playerAudio;
    PlayerController playerController;

    bool isDead; // 죽었나 확인
    bool damaged; // 데미지를 입었나 확인

    private void Awake()
    {
        //anim = GetComponent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();

        currentHealth = startingHealth;
    }

    private void Update()
    {
        if(damaged)
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

        // playerAudio.Play();

        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void Death()
    {
        isDead = true;

        //anim.SetTrigger("Dead");
        //playerAuydio.clip = deathClip;
        //playerAudio.Play();

        playerController.enabled = false;
    }
}
