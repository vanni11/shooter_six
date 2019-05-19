using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
	NavMeshAgent nav;
	GameObject player;

	private void Awake()
	{
		player = GameObject.Find("Player");
		nav = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		nav.SetDestination(player.transform.position);
	}
}
