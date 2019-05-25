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
		int randomNum = Random.Range(0, enemyPrefab.Capacity);
		Instantiate(enemyPrefab[randomNum], spawnPoints[randomNum]);
	}
}
