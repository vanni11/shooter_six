using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    bool playerInRange;
    float timer;

    EnemyHealth enemyHealth;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>(); // playerHealth의 TakeDamage를 호출하기 위함
        //anim = GetComponent<Animator>(); //캡슐로 해서 우선 주석

        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 사거리에 들어온것이 플레이어인지 확인
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        if(playerHealth.currentHealth <= 0)
        {
			//anim.SetTrigger("PlayerDead"); //캡슐로 해서 우선 주석
		}
	}

    void Attack()
    {
        timer = 0.0f;

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
