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
		player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		if(nav.isActiveAndEnabled)
			nav.SetDestination(player.transform.position);
	}
}
