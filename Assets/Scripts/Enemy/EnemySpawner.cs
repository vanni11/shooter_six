using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public int spawnDelay = 2;
	public List<GameObject> enemyPrefab = new List<GameObject>();
	public List<Transform> spawnPoints = new List<Transform>();

	private void Start()
	{
		InvokeRepeating("Spawn", 1f, spawnDelay);
	}

	private void Spawn()
	{
		int randomNum1 = Random.Range(0, enemyPrefab.Capacity - 1); //나오는 적들 랜덤(보스빼고)
		int randomNum2 = Random.Range(0, enemyPrefab.Capacity); //나오는 장소
		int randomNum3 = Random.Range(1, 11); //1~10중에 선택됨
		if(randomNum3 == 10) // 1/10의 확률로 보스등장
			Instantiate(enemyPrefab[4], spawnPoints[randomNum2]);
		else
			Instantiate(enemyPrefab[randomNum1], spawnPoints[randomNum2]);
	}
}
