using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
	NavMeshAgent nav;
	GameObject player;

	private void Awake()
	{
		nav = GetComponent<NavMeshAgent>();
		player = GameObject.Find("Player");
	}

	private void Update()
	{
		nav.SetDestination(player.transform.position);
	}
}
