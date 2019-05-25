using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public List<GameObject> enemyPrefab = new List<GameObject>();
	public List<Transform> spawnPoints = new List<Transform>();

	private void Start()
	{
		InvokeRepeating("Spawn", 1f, 5f);
	}

	private void Spawn()
	{
		int randomNum = Random.Range(0, 3);
		Instantiate(enemyPrefab[randomNum], spawnPoints[randomNum]);
	}
}
